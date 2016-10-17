/**-----------------------------------------------------------------
* @explanation:报事报修主界面布局
* @created：rainday
* @create time：2015/11/19 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Community.Layout', {
    alternateClassName: 'property_community_layout',
    extend: 'Ext.List',
    xtype: 'property_community_layout',
    itemHeight: 100,
    config: {
        title: '社区资讯',
        fullscreen: true,
        cls: 'list_wf',
        disableSelection: true,
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="menuList" style="<tpl if="needsIcon">background-color:#fff</tpl>">',
                '<tpl if="needsIcon"><img width="23" height="23" style="margin: 0 5px 0 10px;" src="resources/images/set/{icon}" align="absmiddle" /></tpl><font style="margin:2px 0 0 0">{name}</font>',
                '<tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px 10px 0 0;"/></tpl>',
            '</div>'
            ),
        store: {
            fields: ['name', 'icon', 'needsIcon', 'url', 'style'],
            data: [
                   { "name": "社区活动", url: 'QST.Property.Community.List', "icon": 'add.png', "needsIcon": true },
                   { "name": "社区公告", url: 'QST.Property.Community.AList', "icon": 'Material.png', "needsIcon": true }
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
                    list.menuTapButton(record.get('url'))
                }
            }
        }
    },
    // 菜单点击事件处理
    menuTapButton: function (url) {
        util.redirectTo(url, "", { parentUrl: "QST.Property.Community.Layout" });
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        util.rightSwipe(me, "Back");
        //加载头部菜单信息
        this.add(this.getHeader());
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
    }
})
