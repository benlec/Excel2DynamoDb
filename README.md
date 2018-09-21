# Excel2DynamodDb, CSV2DynamoDb

Simple tool to import data from Excel or CSV file to DynamoDb (C# Console App)

Problems you can face when trying to import data from Excel/CSV to Amazon DynamoDb:

* Pay attention to the format you select when saving Excel to CSV. Choose [CSV UTF-8]
* Make sur the dataType for your PartitionKey in DynamoDb matches the dataType selected when imported (see code)


#Excel2DynamoDb, #CSV2DynamoDb
