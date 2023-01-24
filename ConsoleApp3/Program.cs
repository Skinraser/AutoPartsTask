// See https://aka.ms/new-console-template for more information
using AngleSharp;
using AngleSharp.Html.Dom;
using ConsoleApp3;
using ConsoleApp3.CRUD;

BrandRepository create = new BrandRepository();
    await create.CreateBrand();


//Parsing.Parsed();
//var config = Configuration.Default.WithDefaultLoader();
//var context = BrowsingContext.New(config);
//var address = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";
//var document = await context.OpenAsync(address);
//var cellSelector = " div.name";
//var cells = document.QuerySelectorAll(cellSelector);
//var titles = cells.Select(m => m.TextContent);

//for (var i = 0; i<1; i++)
//{
//    Console.WriteLine(titles.First());
//    var cell = document.QuerySelectorAll("div.id a").OfType<IHtmlAnchorElement>();
//    address = cell.Select(x => x.Href).First();
//    document = await context.OpenAsync(address);
//    var cell1 = document.QuerySelectorAll("div.modelCode");
//    var title1 = cell1.Select(m => m.TextContent);
//    for (var j = 0; j < 1; j++)
//    {
//        Console.WriteLine(title1.First());
//        var cell0 = document.QuerySelectorAll("div.modelCode a").OfType<IHtmlAnchorElement>();
//        address = cell0.Select(x => x.Href).First();
//        document = await context.OpenAsync(address);
//        var cell2 = document.QuerySelectorAll("div.name");
//        var title2 = cell2.Select(m => m.TextContent);
//        for (int k = 0; k < 1; k++)
//        {
//            Console.WriteLine(title2.First());
//            var cell01 = document.QuerySelectorAll("div.name a").OfType<IHtmlAnchorElement>();
//            address = cell01.Select(x => x.Href).First();
//            document = await context.OpenAsync(address);
//            var cell3 = document.QuerySelectorAll("div.name");
//            var title3 = cell3.Select(m => m.TextContent);
//            for (int l = 0; l < 1; l++)
//            {
//                Console.WriteLine(title3.First());
//            }
//        }
//    }
//}

