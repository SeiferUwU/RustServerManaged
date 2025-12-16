using System;

namespace Facepunch.Models.Database;

[JsonModel]
internal class QueryResponse
{
	public string Content;

	public string Id;

	public DateTime Created;

	public DateTime Updated;

	public string AuthorId;

	public string AuthType;
}
