Similar Project
http://www.bratched.com/en/home/aspnet-mvc/62-how-we-do-aspnet-mvc.html

NuGet
Add-BindingRedirect -> Fix older version references
http://blog.davidebbo.com/2011/01/nuget-versioning-part-3-unification-via.html

StructureMap Tutorial
http://www.kevinwilliampang.com/2010/04/06/setting-up-asp-net-mvc-with-fluent-nhibernate-and-structuremap/

Windsor Tutorial
http://docs.castleproject.org/Windsor.Windsor-tutorial-ASP-NET-MVC-3-application-To-be-Seen.ashx

Fleunt Mapping Doc
http://wiki.fluentnhibernate.org/Fluent_mapping

//Component(x => x.Price, m => m.Map(x => x.Value, "Price"));
//HasMany(x => x.Pricings).LazyLoad().Cascade.All();
//HasMany(x => x.Details).Cascade.All();
//HasMany(x => x.Details)
//    .Not.KeyNullable()
//    .Cascade
//    .AllDeleteOrphan();
//HasMany(x => x.SelectableFeatures)
//    .Not.KeyNullable()
//    .Cascade
//    .AllDeleteOrphan()
//    .KeyColumn("DigitalPhonePlansBase_id");
//HasManyToMany(x => x.IncludedFeatures)
//    .Table("DigitalPhonePlanIncludedFeatures")
//    .ParentKeyColumn("Offering_id")
//    .ChildKeyColumn("DigitalPhoneFeatureId");
//HasManyToMany(x => x.Channels)
//    .Table("TelevisionPackageChannels")
//    .ParentKeyColumn("TelevisionPackageId")
//    .ChildKeyColumn("TelevisionChannelId")
//    .AsSet();

Enabling Unobtrusive Ajax
source: http://bradwilson.typepad.com/blog/2010/10/mvc3-unobtrusive-validation.html

use Web.config:

<configuration>
    <appSettings>
        <add key="ClientValidationEnabled" value="true"/>
        <add key="UnobtrusiveJavaScriptEnabled" value="true"/>
    </appSettings>
</configuration>

OR Code:

HtmlHelper.ClientValidationEnabled = true;
HtmlHelper.UnobtrusiveJavaScriptEnabled = true;

Using code to turn these features on or off actually behaves contextually. If those lines of code are present in your Global.asax file, then it turns unobtrusive JavaScript and client validation on or off for the whole application. If they appear within your controller or view, on the other hand, it will turn the features on or off for the current action only.

In addition to setting the flag, you will also need to include three script files:
jQuery (jquery-1.4.4.min.js)
jQuery Validate (jquery.validate.min.js)
MVC plugin (jquery.validate.unobtrusive.min.js)

An interesting note: since there is no actual JavaScript being emitted when you use unobtrusive client validation, if you forget to include the scripts, you won’t see any errors when loading the page; the form values will simply not validate on the client side.