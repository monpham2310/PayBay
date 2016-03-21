using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using PayBay.Utilities.Common;
using PayBay.Model;

namespace PayBay.Services.MobileServices.InboxSocketIO
{
    public class InboxIO
    {
        //private static InboxIO m_Instance = null;
        private static string HostURL = "http://immense-reef-32079.herokuapp.com/";

        //public static InboxIO Instance
        //{
        //    get
        //    {
        //        return m_Instance ?? (m_Instance = new InboxIO(HostURL, 0));
        //    }

        //    protected set
        //    {
        //        m_Instance = value;
        //    }
        //}

        public static Socket _socket;
        


    }
}
