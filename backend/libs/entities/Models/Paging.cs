namespace CoEvent.Entities.Models;

/// <summary>
/// Page abstract class, provides a model for paging.
/// </summary>
public class Paging<T> : PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - The page number.
  /// </summary>
  public int? Total { get; set; }

  /// <summary>
  /// get/set - Collection of items for this page.
  /// </summary>
  public List<T> Items { get; set; } = new List<T>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a Paging object, initializes with specified parameters.
  /// </summary>
  /// <param name="page"></param>
  /// <param name="quantity"></param>
  /// <param name="items"></param>
  /// <param name="total"></param>
  public Paging(int page, int quantity, IEnumerable<T> items, int? total = null)
  {
    this.Page = page;
    this.Quantity = quantity;
    this.Items = new List<T>(items);
    this.Total = total;
  }

  /// <summary>
  /// Creates a new instance of a Paging object, initializes with specified parameters.
  /// </summary>
  /// <param name="filter"></param>
  /// <param name="items"></param>
  public Paging(PageFilter filter, IEnumerable<T> items) : this(filter.Page, filter.Quantity, items)
  {
  }

  /// <summary>
  /// Creates a new instance of a Paging object, initializes with specified parameters.
  /// </summary>
  /// <param name="filter"></param>
  /// <param name="items"></param>
  /// <param name="total"></param>
  public Paging(PageFilter filter, IEnumerable<T> items, int total) : this(filter.Page, filter.Quantity, items, total)
  {
  }

  /// <summary>
  /// Creates a new instance of a Paging object, initializes with specified parameters.
  /// </summary>
  /// <param name="items"></param>
  public Paging(IEnumerable<T> items) : this(1, items.Count(), items)
  {
  }
  #endregion
}
