using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PharmacyApp.Domain.Entity
{
    [Table("Notifications")]
    public class Notifications 
    {
        [Key]
        public long NotificationId { get; set; }        
        public bool Isread { get; set; }
        public string MsgText { get; set; }
        public int UserId { get; set; }
        public long? CommentId { get; set; }
        public long? LikeId { get; set; }
        public long? CommentLikeId { get; set; }
        public long? ReplyLikeId { get; set; }
        public long? RequestId { get; set; }
        public long? MoodId { get; set;}
        public long? PostId { get; set;}
    }
}
