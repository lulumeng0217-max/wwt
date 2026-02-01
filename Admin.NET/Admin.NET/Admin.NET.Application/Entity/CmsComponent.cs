using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.NET.Application.Entity;

/// <summary>
/// CMS page component instance
/// </summary>
[SugarTable("CmsComponent")]
public class CmsComponent : EntityBaseTenantDel
{

    /// <summary>
    /// Associated page ID
    /// </summary>
    [SugarColumn]
    public long PageId { get; set; }

    /// <summary>
    /// Component code
    /// </summary>
    [SugarColumn()]
    public long ComponentTypeId { get; set; }

    /// <summary>
    /// Pid
    /// </summary>
    [SugarColumn()]
    public long Pid { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    [SugarColumn(Length = 1000)]
    public string Props { get; set; }
    /// <summary>
    /// JSON schema
    /// </summary>
    [SugarColumn(Length = 500)]
    public string? Styles { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    [SugarColumn]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// Whether component is visible
    /// </summary>
    [SugarColumn]
    public bool IsVisible { get; set; } = true;



}

