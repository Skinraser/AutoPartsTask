Select *
From [AutoPartsTask].[dbo].[SubParts]
Join [AutoPartsTask].[dbo].[PrimaryParts] on PrimaryPartId = [AutoPartsTask].[dbo].[PrimaryParts].Id
Join [AutoPartsTask].[dbo].[Complectations] on ComplectationId = [AutoPartsTask].[dbo].[Complectations].Id
Join [AutoPartsTask].[dbo].[CarModels] on CarModelId = [AutoPartsTask].[dbo].[CarModels].Id
Join [AutoPartsTask].[dbo].[Markets] on MarketId = [AutoPartsTask].[dbo].[Markets].Id
Join [AutoPartsTask].[dbo].[Brands] on BrandId = [AutoPartsTask].[dbo].[Brands].Id