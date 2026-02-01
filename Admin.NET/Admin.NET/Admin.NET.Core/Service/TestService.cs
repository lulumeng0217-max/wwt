//using Aop.Api.Domain;
//using Microsoft.AspNetCore.Components.Rendering;
//using NewLife.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Admin.NET.Core.Service;

///// <summary>
///// 系统用户菜单快捷导航服务 🧩
///// </summary>
//[ApiDescriptionSettings(Order = 445)]

//public class TestService : IDynamicApiController, ITransient
//{
//    /// <summary>
//    /// backend page
//    /// </summary>
//    /// <returns></returns>
//    public async Task<List<CmsPage>> GetPageList()
//    {
//        var homePage = new CmsPage
//        {
//            Id = 1001,
//            Name = "Home Page",
//            Status = "published",
//            Template = "default",
//            LastUpdated = DateTime.Parse("2025-01-15T10:30:00Z"),

//            RequestPath = "/",
//            RealPath = "pages/index",
//            IsDynamic = false,

//            Title = "Wendy Wu Tours | Award-Winning Tour Specialist",
//            Description = "Discover fully inclusive tours with New Zealand's award-winning specialist.",
//            Keywords = "tours, new zealand, travel, wendy wu, japan tours, inclusive packages",
//            CanonicalUrl = "https://www.wendywutours.co.nz/",
//            Robots = "index, follow",

//            OgTitle = "Wendy Wu Tours - Exceptional Travel Experiences",
//            OgImage = "https://cdn.wendywutours.com/assets/og-home.jpg",
//            OgType = "website",

//            PageConfig = new PageConfig
//            {
//                EnableHeader = true,
//                EnableFooter = true,
//                BackgroundColor = "#ffffff",
//                CustomCssClass = "home-page-wrapper"
//            },

//            //Components = BuildCommonComponents(includeAlertBar: true)
//        };

//        var aboutPage = new CmsPage
//        {
//            Id = 1002,
//            Name = "About Page",
//            Status = "published",
//            Template = "default",
//            LastUpdated = DateTime.Parse("2025-01-15T10:30:00Z"),

//            RequestPath = "/about",
//            RealPath = "pages/about",
//            IsDynamic = false,

//            Title = "Wendy Wu Tours | About Us",
//            Description = "Learn more about Wendy Wu Tours, our history and our award-winning travel philosophy.",
//            Keywords = "about wendy wu tours, travel experts, tour specialists",
//            CanonicalUrl = "https://www.wendywutours.co.nz/about",
//            Robots = "index, follow",

//            OgTitle = "About Wendy Wu Tours",
//            OgImage = "https://cdn.wendywutours.com/assets/og-home.jpg",
//            OgType = "website",

//            PageConfig = new PageConfig
//            {
//                EnableHeader = true,
//                EnableFooter = true,
//                BackgroundColor = "#ffffff",
//                CustomCssClass = "home-page-wrapper"
//            },

//            //Components = BuildCommonComponents(includeAlertBar: false)
//        };

//        return new List<CmsPage> { homePage, aboutPage };
//    }

//    public async Task<List<CmsComponent>> GetCmsComponent()
//    {
//        var list = new List<CmsComponent>();
//        list.Add(new CmsComponent
//        {
//            Id = "comp_unique_001",
//            Type = "GlobalAlertBar",
//            SortOrder = 1,
//            IsVisible = true,
//            Props = new
//            {
//                text = "Coronavirus – We are here for you. Read the latest facts and how your travel plans are safe with Wendy Wu Tours",
//                linkText = "Read more »",
//                linkUrl = "/safety-updates",
//                bgColor = "#00a699",
//                textColor = "#ffffff",
//                icon = "info-circle"
//            }
//        });
//        list.Add(new CmsComponent
//        {
//            Id = "comp_unique_002",
//            Type = "TextFeatureSection",
//            SortOrder = 2,
//            IsVisible = true,
//            Props = new
//            {
//                title = "New Zealand's Award-Winning Tour Specialist",
//                features = new[]
//                {
//                "Fully inclusive tours",
//                "Visa service included",
//                "More than 26 years of expertise",
//                "Outstanding tour guides",
//                "Book with confidence"
//            },
//                textAlign = "center"
//            },
//            Styles = new
//            {
//                PaddingTop = "4rem",
//                PaddingBottom = "4rem",
//                BackgroundColor = "#fafafa"
//            }
//        });
//        list.Add(new CmsComponent
//        {
//            Id = "comp_unique_003",
//            Type = "OffersSection",
//            SortOrder = 3,
//            IsVisible = true,
//            Props = new
//            {
//                title = "Offers, Events & Inspiration",
//                columns = 4
//            }
//        });
//        return list;
//    }



//    /// <summary>
//    /// fornt page
//    /// </summary>
//    /// <returns></returns>
//    public async Task<List<CmsPage>> GetBlockList()
//    {

//        var homePage = new CmsPage
//        {
//            Id = 1001,
//            Name = "Home Page",
//            Status = "published",
//            Template = "default",
//            LastUpdated = DateTime.Parse("2025-01-15T10:30:00Z"),

//            RequestPath = "/",
//            RealPath = "pages/index",
//            IsDynamic = false,

//            Title = "Wendy Wu Tours | Award-Winning Tour Specialist",
//            Description = "Discover fully inclusive tours with New Zealand's award-winning specialist.",
//            Keywords = "tours, new zealand, travel, wendy wu, japan tours, inclusive packages",
//            CanonicalUrl = "https://www.wendywutours.co.nz/",
//            Robots = "index, follow",

//            OgTitle = "Wendy Wu Tours - Exceptional Travel Experiences",
//            OgImage = "https://cdn.wendywutours.com/assets/og-home.jpg",
//            OgType = "website",

//            PageConfig = new PageConfig
//            {
//                EnableHeader = true,
//                EnableFooter = true,
//                BackgroundColor = "#ffffff",
//                CustomCssClass = "home-page-wrapper"
//            },

//            Components = BuildCommonComponents(includeAlertBar: true)
//        };

//        var aboutPage = new CmsPage
//        {
//            Id = 1002,
//            Name = "About Page",
//            Status = "published",
//            Template = "default",
//            LastUpdated = DateTime.Parse("2025-01-15T10:30:00Z"),

//            RequestPath = "/about",
//            RealPath = "pages/about",
//            IsDynamic = false,

//            Title = "Wendy Wu Tours | About Us",
//            Description = "Learn more about Wendy Wu Tours, our history and our award-winning travel philosophy.",
//            Keywords = "about wendy wu tours, travel experts, tour specialists",
//            CanonicalUrl = "https://www.wendywutours.co.nz/about",
//            Robots = "index, follow",

//            OgTitle = "About Wendy Wu Tours",
//            OgImage = "https://cdn.wendywutours.com/assets/og-home.jpg",
//            OgType = "website",

//            PageConfig = new PageConfig
//            {
//                EnableHeader = true,
//                EnableFooter = true,
//                BackgroundColor = "#ffffff",
//                CustomCssClass = "home-page-wrapper"
//            },

//            Components = BuildCommonComponents(includeAlertBar: false)
//        };

//        return new List<CmsPage> { homePage, aboutPage };
//    }
//    private static List<CmsComponent> BuildCommonComponents(bool includeAlertBar)
//    {
//        var list = new List<CmsComponent>();

//        if (includeAlertBar)
//        {
//            list.Add(new CmsComponent
//            {
//                Id = "comp_unique_001",
//                Type = "GlobalAlertBar",
//                SortOrder = 1,
//                IsVisible = true,
//                Props = new
//                {
//                    text = "Coronavirus – We are here for you. Read the latest facts and how your travel plans are safe with Wendy Wu Tours",
//                    linkText = "Read more »",
//                    linkUrl = "/safety-updates",
//                    bgColor = "#00a699",
//                    textColor = "#ffffff",
//                    icon = "info-circle"
//                }
//            });
//        }

//        list.Add(new CmsComponent
//        {
//            Id = "comp_unique_002",
//            Type = "TextFeatureSection",
//            SortOrder = 2,
//            IsVisible = true,
//            Props = new
//            {
//                title = "New Zealand's Award-Winning Tour Specialist",
//                features = new[]
//                {
//                "Fully inclusive tours",
//                "Visa service included",
//                "More than 26 years of expertise",
//                "Outstanding tour guides",
//                "Book with confidence"
//            },
//                textAlign = "center"
//            },
//            Styles = new 
//            {
//                PaddingTop = "4rem",
//                PaddingBottom = "4rem",
//                BackgroundColor = "#fafafa"
//            }
//        });

//        list.Add(new CmsComponent
//        {
//            Id = "comp_unique_003",
//            Type = "OffersSection",
//            SortOrder = 3,
//            IsVisible = true,
//            Props = new
//            {
//                title = "Offers, Events & Inspiration",
//                columns = 4
//            }
//        });

//        return list;
//    }
//}

//public class CmsPage
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string Status { get; set; } = string.Empty;
//    public string Template { get; set; } = string.Empty;
//    public DateTime LastUpdated { get; set; }

//    public string RequestPath { get; set; } = string.Empty;
//    public string RealPath { get; set; } = string.Empty;
//    public bool IsDynamic { get; set; }

//    // SEO
//    public string Title { get; set; } = string.Empty;
//    public string Description { get; set; } = string.Empty;
//    public string Keywords { get; set; } = string.Empty;
//    public string CanonicalUrl { get; set; } = string.Empty;
//    public string Robots { get; set; } = string.Empty;

//    // OpenGraph
//    public string OgTitle { get; set; } = string.Empty;
//    public string OgImage { get; set; } = string.Empty;
//    public string OgType { get; set; } = string.Empty;

//    public PageConfig PageConfig { get; set; } = new();

//    public List<CmsComponent> Components { get; set; } = new();
//}


//public class PageConfig
//{
//    public bool EnableHeader { get; set; }
//    public bool EnableFooter { get; set; }
//    public string BackgroundColor { get; set; } = "#ffffff";
//    public string CustomCssClass { get; set; } = string.Empty;
//}

//public class CmsComponent
//{
//    public string Id { get; set; } = string.Empty;
//    public string Type { get; set; } = string.Empty;
//    public int SortOrder { get; set; }
//    public bool IsVisible { get; set; }
//    public object? Props { get; set; }
//    public object? Styles { get; set; }
//}



