namespace PmServiceNetCode.DTOs
{
    public sealed record IradatQuery(
      string TableName,
      string NameColumn,
      string CodeColumn,
      int NoeTajhizId,
      IReadOnlyList<int> IradIds);

}
