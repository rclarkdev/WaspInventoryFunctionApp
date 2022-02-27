using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WaspInventory.Tests
{
    public class TestFactory
    {
        public static IEnumerable<object[]> RemovalRequestData()
        {

            string[] removalRequestData = {
                @"
                    {'Orders': [
                        {
                            'LineItems': [
                                {
                                    'ItemNumber': 'MUSB-ACCHARGER',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 5,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                },
                                {
                                    'ItemNumber': 'MUSB-CARCHARGER',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 7,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                }
                            ]
                        },
                        {
                            'LineItems': [
                                {
                                    'ItemNumber': 'MUSB-ACCHARGER',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 8,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                },
                                {
                                    'ItemNumber': 'MUSB-CARCHARGER',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 10,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                },
                                {
                                    'ItemNumber': 'FALL-DET-KIT-BLK',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 4,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                }
                            ]
                        },
                        {
                            'LineItems': [
                                 {
                                    'ItemNumber': 'FALL-DET-KIT-BLK',
                                    'DateRemoved': '2021-12-03',
                                    'SiteName': 'CB Warehouse',
                                    'Quantity': 9,
                                    'LocationCode': '',
                                    'DateCode': '2021-12-03'
                                }
                            ]
                        }
                    ]}"
            };

            return new List<object[]>() { removalRequestData };

        }

        public static string InventorySearchResponseData()
        {

            return
                @"{'Data': 
                [
                    {
                        'ItemNumber': 'FALL-DET-KIT-BLK',
                        'ItemDescription': 'FALL DETECTION KIT BLK',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 1,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'RR-5',
                        'TotalAvailable': 22.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 0.0000,
                        'ListPrice': 0.0000,
                        'SalesPrice': 0.0000,
                        'CategoryDescription': 'ACC',
                        'ManufacturerName': 'GREATCALL',
                        'StockingUnit': 'each',
                        'TaxCode': 'Tax',
                        'AlternateItemNumber': 'FALL-DET-KIT-BLK'
                    },
                    {
                        'ItemNumber': 'FALL-DET-KIT-BLK',
                        'ItemDescription': 'FALL DETECTION KIT BLK',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 2,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'IS-2',
                        'TotalAvailable': 60.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 0.0000,
                        'ListPrice': 0.0000,
                        'SalesPrice': 0.0000,
                        'CategoryDescription': 'ACC',
                        'ManufacturerName': 'GREATCALL',
                        'StockingUnit': 'each',
                        'TaxCode': 'Tax',
                        'AlternateItemNumber': 'FALL-DET-KIT-BLK'
                    },
                    {
                        'ItemNumber': 'MUSB-ACCHARGER',
                        'ItemDescription': 'MICRO USB AC CHARGER 13085 or 19421',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 1,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'RR-5',
                        'TotalAvailable': 49.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 2.2000,
                        'ListPrice': 19.0000,
                        'SalesPrice': 19.0000,
                        'CategoryDescription': 'CRG',
                        'ManufacturerName': 'MOBILEISTIC',
                        'StockingUnit': 'each',
                        'TaxCode': 'Non',
                        'AlternateItemNumber': 'MUSB-ACCHARGER'
                    },
                    {
                        'ItemNumber': 'MUSB-ACCHARGER',
                        'ItemDescription': 'MICRO USB AC CHARGER 13085 or 19421',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 2,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'IS-11',
                        'TotalAvailable': 21.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 2.2000,
                        'ListPrice': 19.0000,
                        'SalesPrice': 19.0000,
                        'CategoryDescription': 'CRG',
                        'ManufacturerName': 'MOBILEISTIC',
                        'StockingUnit': 'each',
                        'TaxCode': 'Non',
                        'AlternateItemNumber': 'MUSB-ACCHARGER'
                    },
                    {
                        'ItemNumber': 'MUSB-ACCHARGER',
                        'ItemDescription': 'MICRO USB AC CHARGER 13085 or 19421',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 2,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'IS-2',
                        'TotalAvailable': 4.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 2.2000,
                        'ListPrice': 19.0000,
                        'SalesPrice': 19.0000,
                        'CategoryDescription': 'CRG',
                        'ManufacturerName': 'MOBILEISTIC',
                        'StockingUnit': 'each',
                        'TaxCode': 'Non',
                        'AlternateItemNumber': 'MUSB-ACCHARGER'
                    },
                    {
                        'ItemNumber': 'MUSB-CARCHARGER',
                        'ItemDescription': 'MICRO USB CAR CHARGER',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 3,
                        'SiteName': 'Corporate Office - San Diego',
                        'LocationCode': 'SDR-1',
                        'TotalAvailable': 16.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 0.9400,
                        'ListPrice': 24.0000,
                        'SalesPrice': 24.0000,
                        'CategoryDescription': 'CRG',
                        'ManufacturerName': 'MOBILEISTIC',
                        'StockingUnit': 'each',
                        'TaxCode': 'Tax',
                        'AlternateItemNumber': 'MUSB-CARCHARGER'
                    },
                    {
                        'ItemNumber': 'MUSB-CARCHARGER',
                        'ItemDescription': 'MICRO USB CAR CHARGER',
                        'CostMethod': 10,
                        'ItemType': 2147483635,
                        'RowNumber': 4,
                        'SiteName': 'CB Warehouse',
                        'LocationCode': 'RR-3',
                        'TotalAvailable': 7.0000,
                        'OnOrder': 0.0000,
                        'Committed': 0.0000,
                        'ItemCost': 0.9400,
                        'ListPrice': 24.0000,
                        'SalesPrice': 24.0000,
                        'CategoryDescription': 'CRG',
                        'ManufacturerName': 'MOBILEISTIC',
                        'StockingUnit': 'each',
                        'TaxCode': 'Tax',
                        'AlternateItemNumber': 'MUSB-CARCHARGER'
                    },
                ]}";
        }
    }
}
