using leaseEase.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.helpers
{

        public class ChatViewModel
        {
            public leaseEase.Domain.Models.User.User User { get; set; }
            public Chat SelectedChat { get; set; }
            public int SelectedChatId { get; set; }
        }

}
