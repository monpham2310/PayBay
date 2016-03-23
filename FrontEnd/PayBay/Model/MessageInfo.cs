using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Model
{
    public class MessageInfo
    {
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public int UserChat { get; set; }
        public Utilities.Common.TYPE Type { get; set; }

        public MessageInfo()
        {

        }

        public MessageInfo(int msgId, int userId, int userChat, Utilities.Common.TYPE type)
        {
            MessageID = msgId;
            UserID = userId;
            UserChat = userChat;
            Type = type;
        }
    }
}
