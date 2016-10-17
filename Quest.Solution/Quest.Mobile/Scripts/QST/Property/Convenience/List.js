/**-----------------------------------------------------------------
* @explanation:报事报修主界面布局
* @created：rainday
* @create time：2015/11/19 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Convenience.List', {
    alternateClassName: 'property_convenience_list',
    extend: 'Ext.List',
    xtype: 'property_convenience_list',
    itemHeight: 100,
    config: {
        fullscreen: true,
        cls: 'list_wf',
        disableSelection: true,
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="menuListX" style="<tpl if="needsIcon">background-color:#fff</tpl>">',
            '<div class="conveniencelistdiv"> <img width="80" height="80" style="margin:0 5px 0 10px;" src="resources/images/set/{icon}" align="absmiddle" /></div >',
            '<div class="div1">',
           ' <div class="name">{name}</div>',
            '<div class="name2"><{m}m</div>',
            '<div class="name3">{area}</div>',
            '<div>',
                //'<tpl if="needsIcon"><img width="60" height="60" style="margin: 0 5px 0 10px;" src="resources/images/set/{icon}" align="absmiddle" /></tpl><font style="margin:2px 0 0 0">{name}</font>',
                //'<tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px 10px 0 0;"/></tpl>',
            '</div>'
            ),
        store: {
            fields: ['name', 'icon', 'needsIcon', 'url', 'style','m','area'],
            data: [
                   { "name": "重庆文军医院", url: 'QST.Property.Payment.Edit', "icon": 'add.png', 'm': "200", 'area': "渝北区", "needsIcon": true },
                   { "name": "重庆文军医院", url: 'QST.Property.Payment.Edit', "icon": 'Material.png', 'm': "200", 'area': "渝北区", "needsIcon": true },
                   { "name": "重庆文军医院", url: 'QST.Property.Payment.Edit', "icon": 'Supplied.png', 'm': "200", 'area': "渝北区", "needsIcon": true },
                   { "name": "重庆文军医院", url: 'QST.Property.Payment.Edit', "icon": 'Material.png', 'm': "200", 'area': "渝北区", "needsIcon": true },
                   { "name": "重庆文军医院", url: 'QST.Property.Payment.Edit', "icon": 'Supplied.png', 'm': "200", 'area': "渝北区", "needsIcon": true },
                   ]
        },
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
            //点击事件
            itemsingletap: function (list, index, target, record, e, eOpts) {
                //if (record.get('needsIcon')) {
                //    list.menuTapButton(record.get('url'), record.get("name"))
                //}
            }
        }
    },
    // 菜单点击事件处理
    menuTapButton: function (url, name) {
        util.redirectTo(url, "", { parentUrl: "QST.Property.Convenience.Layout", name: name });
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        util.rightSwipe(me, "Back");

    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params)
            this.backUrl = params.parentUrl;
    }
})
