/**-----------------------------------------------------------------
* @explanation:报事报修主界面布局
* @created：rainday
* @create time：2015/11/19 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Payment.List', {
    alternateClassName: 'property_payment_list',
    extend: 'Ext.List',
    xtype: 'property_payment_list',
    itemHeight: 100,
    config: {
        title: '重庆市',
        fullscreen: true,
        cls: 'list_wf',
        disableSelection: true,
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="menuList" style="<tpl if="needsIcon">background-color:#fff</tpl>">',
            '<div class="img"><tpl if="needsIcon"><img width="23" height="23" style="margin: 0 5px 0 10px;" src="resources/images/property/payment/{icon}" align="absmiddle" /></tpl><font style="margin:2px 0 0 0">{name}</font></div>',
            '<div class="add">可添加<tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px 10px 0 0;"/></tpl></div>',
                //'<div><tpl if="needsIcon"><img width="23" height="23" style="margin: 0 5px 0 10px;" src="resources/images/set/{icon}" align="absmiddle" /></tpl><font style="margin:2px 0 0 0">{name}</font></div>',
                //'<div><tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px 10px 0 0;"/></tpl><div>',
            '</div>'
            ),
        store: {
            fields: ['name', 'icon', 'needsIcon', 'url', 'style'],
            data: [
                   { "name": "物业费", url: 'QST.Property.Payment.Edit', "icon": 'property2x.png', "needsIcon": true },
                   { "name": "停车费", url: 'QST.Property.Payment.Edit', "icon": 'carpark2x.png', "needsIcon": true },
                   { "name": "有线电视费", url: 'QST.Property.Payment.Edit', "icon": 'cableTV2x.png', "needsIcon": true },
                   { "name": "有线宽带", url: 'QST.Property.Payment.Edit', "icon": 'broadband2x.png', "needsIcon": true },
                   { "name": "水费", url: 'QST.Property.Payment.Edit', "icon": 'electricity2x.png', "needsIcon": true },
                   { "name": "电费", url: 'QST.Property.Payment.Edit', "icon": 'electricity2x.png', "needsIcon": true },
                   { "name": "煤气费", url: 'QST.Property.Payment.Edit', "icon": 'gas2x.png', "needsIcon": true }
            ]
        },
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
            //点击事件
            itemsingletap: function (list, index, target, record, e, eOpts) {
                if (record.get('needsIcon')) {
                    list.menuTapButton(record.get('url'), record.get("name"))
                }
            }
        }
    },
    // 菜单点击事件处理
    menuTapButton: function (url,name) {
        util.redirectTo(url, "", { parentUrl: "QST.Property.Payment.Layout", name: name });
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        util.rightSwipe(me, "Back");
        ////加载头部菜单信息
        //this.add(this.getHeader());
        ////头部标签
        //this.add(this.getTop());
        ////底部标签
        //this.add(this.getBottom());
      
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params)
            this.backUrl = params.parentUrl;
    },
    //头部菜单信息(private)
    getHeader: function () {
        var me = this;
        if (!this._homeHeaderBar) {
            this._homeHeaderBar = Ext.create("app.user.NavigationBar", {
                title: me._title,
                docked: 'top',
                items: [
                    {
                        iconCls: 'arrow_left',
                        action: 'Back',
                        cls: 'nbutton',
                        align: 'left',
                        handler: function (but) {
                            me.fireEvent('Back', but, me);
                        }
                    }
                ],
            });
        }
        return this._homeHeaderBar;
    },
    //头部标签
    getTop: function () {
        var me = this;
        if (!me._getTop) {
            me._getTop = Ext.create("Ext.Panel", {
                layout: 'hbox',
                scrollDock: 'top',
                docked: 'top',
                cls: 'home_msg',
                html:'缴费账户'
            });
        }
        return me._getTop;
    },
    //底部标签
    getBottom: function () {
        var me = this;
        if (!me._getBottom) {
            me._getBottom = Ext.create("Ext.Panel", {
                layout: 'hbox',
                scrollDock: 'bottom',
                docked: 'bottom',
                cls: 'bottom',
                //html: '缴费历史|帮助中心',
                items: [{
                    xtype: 'button',
                    text: '缴费历史',
                    handler: function (but) {
                        util.redirectTo("QST.Property.Payment.HList", "", { parentUrl: "QST.Property.Payment.List"});
                    }
                }, {
                    xtype: 'button',
                    text: '帮助中心',
                    handler: function (but) {
                        util.redirectTo("", "", { parentUrl: "QST.Property.Payment.List" });
                    }
                }]
            });
        }
        return me._getBottom;
    },
})
