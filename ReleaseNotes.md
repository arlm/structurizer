#Release notes

## v1.0.0 - 2016-11-28
Might be weird you think to set 1.0.0 so soon. Remember that the code comes from two generations. One generation from the PineCone project and one from SisoDB. Structurizer is based upon this. But with a lot of stuff removed.

With this release focus has been on performance and resource usage. Read more here: http://danielwertheim.se/structurizer-improvements/

## v0.2.0 - 2016-11-13
- Breaking changes in API to simplify usage of Structurizer.
- Added possibility of per type define `IndexMode.Inclusive|Exclusive`.
- `StructureProperty.SetValue` is removed. Structurizer is about reading values.
- Indexes paths has now changed. When part of something that is enumerable (arrays, lists, collections etc.) there will be an indicator telling at what position the value was.

You get:

```
OrderLines[0].ArticleNo="123454"
OrderLines[0].Qty=1
OrderLines[1].ArticleNo="123454"
OrderLines[1].Qty=3
```

instead off:

```
OrderLines.ArticleNo="123454"
OrderLines.Qty=1
OrderLines.ArticleNo="123454"
OrderLines.Qty=3
```

## v0.1.0 - 2016-05-24
First release, after import from PineCone. Heavily reduced when it comes to features.