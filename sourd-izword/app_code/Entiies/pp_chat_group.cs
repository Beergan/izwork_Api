using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("pp_chat_group")]
public class pp_chat_group : EntityBase
{
	public string Domain { get; set; }
	public DateTime CreatedDate { get; set; }
}

