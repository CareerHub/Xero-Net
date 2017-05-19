Xero-NetStandard
![Build Status](https://ci.appveyor.com/api/projects/status/github/visualeyes/xero-netstandard?branch=master&svg=true) 
[![Nuget Version](https://img.shields.io/nuget/v/Xero-NetStandard.svg)](https://www.nuget.org/packages/Xero-NetStandard/) 
========
A skinny wrapper of the Xero API. All third party libraries are included as source code.

* [What is supported?](#what-is-supported)
* [Things to note](#things-to-note)
* [Samples](#samples)
* [Querying](#querying)
* [Applications](#application)
* [Authenticators](#authenticators)
* [Token Stores](#token-stores)
* [Serialization](#serialization)
* [License](#license)

## What is supported?
* Accounts - Find and Create
* Bank Transactions - Create, Find and Update
* Bank Transfers - Create, Find and Update
* Branding Themes - Find
* Contacts - Create, Find and Update
* Credit Notes - Create, Find, Update and as PDF
* Currencies - Find
* Employees - Create, Find and Update
* Expense Claims - Create, Find and Update
* Invoices - Create, Find, Update and as PDF
* Journals - Find
* Manual Journals - Create, Find and Update
* Organisation - Find
* Payments - Create and Find
* Receipts - Create, Find and Update
* Repeating Invoices - Find
* Tax Rates - Create, Find and Update
* Users - Find

## Things to note
* You will need an instance of the API per user. The user is stored as part of the API instance.
* The library tries to do as little as possible and provides a basis to be extended. There are examples of TokenStores, Authenticators and Application types. These examples provide enough to get you going, but are not a complete solution to all your needs. You will need to adapt them for your own use and situation. Private application will work out of the box, as they do not have to deal with tokens and OAuth.
* The HTTP verbs are not used in the public part of the API. Create, Update and Find are used instead. This seperates the implementation from the the intent.
* Invoices and Contacts support pagination. Unlike the RESTful API, paging is on and defaulted to page 1. See the Counts or Creation code examples for how to use the Page method to get all items.
* Four decimal places are supported for Bank Transactions, Credit Notes, Invoices, Payments, Repeating Invoices and Receipts. Unlike the RESTful API, it is on by default.
* Contacts support including archived contacts. Like the RESTful API, this is off by default. Use IncluceArchived(true) to include them.

## Samples
There are samples for each of the API endpoints. These have been done as console applications and also a collection of [NUnit]("http://nunit.org") tests. See the README for each of the executable and test assemblies.

## Querying
There are simple filters on different endpoints.

* ModifiedSince
* Where
* Or
* And
* OrderBy
* OrderByDescending
* Page
* Offset

They are used in a [Fluent](http://en.wikipedia.org/wiki/Fluent_interface) way, but are not LINQ. They simply create a query for the URL passed to the API. Nested queries are not handled using the syntax. Or and And need to come after a Where statement. OrderBy, OrderByDescending and Page can come anywhere.

	var invoices = await xeroApi.Invoices  
		.ModifiedSince(new DateTime(2014, 1, 31))  
		.Where("Total > 3500.0")  
		.And("Total < 10000.0")  
		.Page(2)  
		.OrderByDescending("DueDate")  
		.FindAsync();

The following gives the same query string to the API as the example above.

	var invoices = await xeroApi.Invoices  
		.Page(2)  
		.OrderByDescending("DueDate")  
		.Where("Total > 3500.0")   
		.And("Total < 10000.0")  
		.ModifiedSince(new DateTime(2014, 1, 31))  
		.FindAsync();

## Authenticators
The application classes all use implementations of IAuthenticator. See [Xero.Api.Infrastructure.OAuth.Authenticator](https://github.com/XeroAPI/Xero-Net/tree/master/source/Xero.Api/Infrastructure/OAuth/Authenticator) namespace for the implementations. The authenticators are used by the base infrastructure to do the heavy lifting of the Xero API authentication.

### PrivateAuthenticator
Uses RSA-SHA1 and a public/private certificate. There are no tokens and each request has to be signed.

### PublicAuthenticator
Uses HMAC-SHA1 and the standard 3-legged [OAuth](http://tools.ietf.org/html/rfc6749) process. Tokens last for 30 minutes and cannot be renewed.

### PartnerAuthenticator
Uses RSA-SHA1 and a Xero provider certificate and then the standard 3-legged [OAuth](http://tools.ietf.org/html/rfc6749) process with an additional signing certificate. Tokens last for 30 minutes and be renewed. Token renewal is not currently handled by this provider, but will be done soon.

### OAuth signing
All the signing is done by a slightly modified version of the Dust library provided by [Ben Biddington](https://github.com/ben-biddington/dust). Source is included.

## Token Stores
The token store implementations are separate and optional. (It is recommended that you do have a store.)

The interface ITokenStore has three methods.

	public interface ITokenStore
	{
		IConsumer Find(string user);
		void Add(IToken token);
		void Delete(IToken token);
	}

You can provide your own implementation to suit the database you are using for your product. Ensure the dates on the token are stored in UTC.

## Serialization
All communication with the [Xero API](http://deverloper.xero.com) is compressed at source. Writing to the API is done with XML. The data model classes have be attributed to give a small XML payload. All communication back from the API is JSON. These details are transparent to the user of the class library.

## License
This software is published under the [MIT License](http://en.wikipedia.org/wiki/MIT_License).

	Copyright (c) 2014 Xero Limited

	Permission is hereby granted, free of charge, to any person
	obtaining a copy of this software and associated documentation
	files (the "Software"), to deal in the Software without
	restriction, including without limitation the rights to use,
	copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the
	Software is furnished to do so, subject to the following
	conditions:

	The above copyright notice and this permission notice shall be
	included in all copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
	EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
	OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
	NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
	HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
	WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
	FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
	OTHER DEALINGS IN THE SOFTWARE.