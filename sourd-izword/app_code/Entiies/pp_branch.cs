using PetaPoco;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Helpers;

[Table("pp_branch")]
public class pp_branch : EntityBase
{
    public int GroupId { get; set; }
    public string BranchGuid { get; set; }
	public string BranchName { get; set; }
	public string MessagesJson { get; set; }

	[Ignore]
	public List<MessageJson> Messages
	{
		get
		{
			if (string.IsNullOrEmpty(MessagesJson))
				return new List<MessageJson>();

			var jsonString = Json.Decode<List<MessageJson>>(MessagesJson);

			return jsonString;
		}
		set
		{
			MessagesJson = Json.Encode(value);
		}
	}
}

[NotMapped]
public class MessageJson
{
	public string Guid { get; set; }
	public string Role { get; set; }
	public string StaffGuid { get; set; }
	public string StaffName { get; set; }
	public string AvatarUrl { get; set; }
	public string Content { get; set; }
	public string ReplyGuid { get; set; }
	public string RepContent { get; set; }
	public string ImageUrl { get; set; }
	public string TimeSent { get; set; }
	public string RepTo { get; set; }
}