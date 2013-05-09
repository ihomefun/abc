﻿////
//   NLog.Targets.Syslog
//   ------------------------------------------------------------------------
//   Copyright 2010 Jesper Hess Nielsen <jesper@graffen.dk>
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
////
namespace NLog.Targets
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Reflection;
   using System.Threading;
   using System.Net;
   using System.Net.Sockets;
   using System.Globalization;

   /// <summary>
   /// This class enables logging to a unix-style syslog server using NLog.
   /// </summary>

   [NLog.Targets.Target("Syslog")]
   public class Syslog : NLog.Targets.Target
   {
      /// <summary>
      /// Initializes a new instance of the Syslog class
      /// </summary>
      public Syslog()
      {
         // Sensible defaults...
         SyslogServer = "127.0.0.1";
         Port = 514;
         Sender = Assembly.GetCallingAssembly().GetName().Name;
         Facility = SyslogFacility.Local1;
      }

      #region Enumerations
      /// <summary>
      /// syslog severities
      /// </summary>
      /// <remarks>
      /// <para>
      /// The syslog severities.
      /// </para>
      /// </remarks>
      public enum SyslogSeverity
      {
         /// <summary>
         /// system is unusable
         /// </summary>
         Emergency = 0,

         /// <summary>
         /// action must be taken immediately
         /// </summary>
         Alert = 1,

         /// <summary>
         /// critical conditions
         /// </summary>
         Critical = 2,

         /// <summary>
         /// error conditions
         /// </summary>
         Error = 3,

         /// <summary>
         /// warning conditions
         /// </summary>
         Warning = 4,

         /// <summary>
         /// normal but significant condition
         /// </summary>
         Notice = 5,

         /// <summary>
         /// informational messages
         /// </summary>
         Informational = 6,

         /// <summary>
         /// debug-level messages
         /// </summary>
         Debug = 7
      };

      /// <summary>
      /// syslog facilities
      /// </summary>
      /// <remarks>
      /// <para>
      /// The syslog facilities
      /// </para>
      /// </remarks>
      public enum SyslogFacility
      {
         /// <summary>
         /// kernel messages
         /// </summary>
         Kernel = 0,

         /// <summary>
         /// random user-level messages
         /// </summary>
         User = 1,

         /// <summary>
         /// mail system
         /// </summary>
         Mail = 2,

         /// <summary>
         /// system daemons
         /// </summary>
         Daemons = 3,

         /// <summary>
         /// security/authorization messages
         /// </summary>
         Authorization = 4,

         /// <summary>
         /// messages generated internally by syslogd
         /// </summary>
         Syslog = 5,

         /// <summary>
         /// line printer subsystem
         /// </summary>
         Printer = 6,

         /// <summary>
         /// network news subsystem
         /// </summary>
         News = 7,

         /// <summary>
         /// UUCP subsystem
         /// </summary>
         Uucp = 8,

         /// <summary>
         /// clock (cron/at) daemon
         /// </summary>
         Clock = 9,

         /// <summary>
         /// security/authorization  messages (private)
         /// </summary>
         Authorization2 = 10,

         /// <summary>
         /// ftp daemon
         /// </summary>
         Ftp = 11,

         /// <summary>
         /// NTP subsystem
         /// </summary>
         Ntp = 12,

         /// <summary>
         /// log audit
         /// </summary>
         Audit = 13,

         /// <summary>
         /// log alert
         /// </summary>
         Alert = 14,

         /// <summary>
         /// clock daemon
         /// </summary>
         Clock2 = 15,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local0 = 16,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local1 = 17,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local2 = 18,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local3 = 19,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local4 = 20,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local5 = 21,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local6 = 22,

         /// <summary>
         /// reserved for local use
         /// </summary>
         Local7 = 23
      }

      #endregion Enumerations

      #region Public properties
      /// <summary>
      /// Sets the IP Address or Host name of your Syslog server
      /// </summary>
      public string SyslogServer { get; set; }

      /// <summary>
      /// Sets the Port number syslog is running on (usually 514)
      /// </summary>
      public int Port { get; set; }

      /// <summary>
      /// Sets the name of the application that will show up in the syslog log
      /// </summary>
      public string Sender { get; set; }

      /// <summary>
      /// Sets the syslog facility name to send messages as (for example, local0 or local7)
      /// </summary>
      public SyslogFacility Facility { get; set; }
      #endregion


      public string AppName { get; set; }

      /// <summary>
      /// This is where we hook into NLog, by overriding the Write method. 
      /// </summary>
      /// <param name="logEvent">The NLog.LogEventInfo </param>
      protected override void Write(LogEventInfo logEvent)
      {
         // Store the current UI culture
         CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
         // Set the current Locale to "en-US" for proper date formatting
         Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

         string passedAppName = "";
         if (logEvent.Parameters != null && logEvent.Parameters.Count() > 0)
         {
            if (logEvent.Parameters[0].ToString() != logEvent.FormattedMessage)
            { passedAppName = logEvent.Parameters[0].ToString(); }
         }
         else if (logEvent.Exception != null)
         {
            if (!string.IsNullOrEmpty(logEvent.Exception.HelpLink))
            { passedAppName = logEvent.Exception.HelpLink; }
         }

         byte[] msg = buildSyslogMessage(Facility, logEvent.Level, DateTime.Now, Sender, logEvent.FormattedMessage, passedAppName);
         sendMessage(SyslogServer, Port, msg);

         // Restore the original culture
         Thread.CurrentThread.CurrentCulture = currentCulture;
      }

      /// <summary>
      /// Performs the actual network part of sending a message
      /// </summary>
      /// <param name="SyslogServer">The syslog server's host name or IP address</param>
      /// <param name="Port">The UDP port that syslog is running on</param>
      /// <param name="msg">The syslog formatted message ready to transmit</param>
      private void sendMessage(string SyslogServer, int Port, byte[] msg)
      {
         string ipAddress = Dns.GetHostAddresses(SyslogServer).FirstOrDefault().ToString();
         UdpClient udp = new UdpClient(ipAddress, Port);
         udp.Send(msg, msg.Length);
         udp.Close();
         udp = null;
      }

      /// <summary>
      /// Mapping between NLog levels and syslog severity levels as they are not exactly one to one. 
      /// </summary>
      /// <param name="logLevel">NLog log level to translate</param>
      /// <returns>SyslogSeverity which corresponds to the NLog level. </returns>
      private SyslogSeverity getSyslogSeverity(LogLevel logLevel)
      {
         if (logLevel == LogLevel.Fatal)
         {
            return SyslogSeverity.Emergency;
         }
         else if (logLevel >= LogLevel.Error)
         {
            return SyslogSeverity.Error;
         }
         else if (logLevel >= LogLevel.Warn)
         {
            return SyslogSeverity.Warning;
         }
         else if (logLevel >= LogLevel.Info)
         {
            return SyslogSeverity.Informational;
         }
         else if (logLevel >= LogLevel.Debug)
         {
            return SyslogSeverity.Debug;
         }
         else if (logLevel >= LogLevel.Trace)
         {
            return SyslogSeverity.Notice;
         }
         else
         {
            return SyslogSeverity.Notice;
         }
      }

      /// <summary>
      /// Builds a syslog-compatible message using the information we have available. 
      /// </summary>
      /// <param name="facility">Syslog Facility to transmit message from</param>
      /// <param name="priority">Syslog severity level</param>
      /// <param name="time">Time stamp for log message</param>
      /// <param name="sender">Name of the subsystem sending the message</param>
      /// <param name="body">Message text</param>
      /// <returns>Byte array containing formatted syslog message</returns>
      private byte[] buildSyslogMessage(SyslogFacility facility, LogLevel logLevel , DateTime time, string sender, string body, string passedAppName)
      {

         SyslogSeverity priority = getSyslogSeverity(logLevel);

         // Get sender machine name
         string machine = System.Net.Dns.GetHostName() + " ";

         // Calculate PRI field
         int calculatedPriority = (int)facility * 8 + (int)priority;
         string pri = "<" + calculatedPriority.ToString() + ">";

         string timeToString = time.ToString("MMM dd HH:mm:ss ");

         if (string.IsNullOrEmpty(passedAppName))
         {
            if (! string.IsNullOrEmpty(AppName))
            {
               sender = AppName;
            }
         }
         else
         {
            sender = passedAppName;
         }

         sender += "/" + logLevel + ": ";

         string[] strParams = { pri, timeToString, machine, sender, body };
         return Encoding.ASCII.GetBytes(string.Concat(strParams));
      }
   }
}
