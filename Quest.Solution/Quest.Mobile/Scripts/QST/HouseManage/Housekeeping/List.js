/**-----------------------------------------------------------------
* @explanation:家政服务界面
* @created：XS
* @create time：2016/10/11
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.HouseManage.Housekeeping.List', {
    alternateClassName: 'housemanage_housekeeping_list',
    extend: 'app.user.NSimpleList',
    xtype: 'housemanage_housekeeping_list',
    itemHeight: 100,
    config: {
        title: config.str.Housekeeping,
        fullscreen: true,
        cls: 'list_wf',
        disableSelection: true,
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="menuList" style="<tpl if="needsIcon">background-color:#fff</tpl>">',
                '<tpl if="needsIcon"><img width="{[this.getWidth(values.iconW)]}" height="{[this.getHeight(values.iconH)]}" style="margin: 5px 10px;" src="resources/images/set/{icon}.png" align="absmiddle" /></tpl>',
                '<font style="margin:2px 0 10px 0">{name} </font>',
                '<tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px 10px 0 0;"/></tpl>',
            '</div>', {
                getWidth: function (iconW) {
                    if (!iconW) {
                        return 23;
                    }
                    if (iconW > 23) {
                        return iconW;
                    } else {
                        return 23;
                    }
                },
                getHeight: function (iconH) {
                    if (!iconH) {
                        return 23;
                    }
                    if (iconH > 23) {
                        return iconH;
                    } else {
                        return 23;
                    }
                }
            }),
        store: {
            fields: ['name', 'icon', 'needsIcon', 'url', 'style', 'iconW', 'iconH'],
            data: [
                { "name": "钟点工", url: 'QST.HouseManage.Housekeeping.PJEdit', "icon": 'password', "needsIcon": true },
                { "name": "月嫂", url: 'QST.HouseManage.Housekeeping.YSEdit', "icon": 'help', "needsIcon": true },
                { "name": "护工", url: 'QST.HouseManage.Housekeeping.CWEdit', "icon": 'question', "needsIcon": true },
                { "name": "住家阿姨", url: 'QST.HouseManage.Housekeeping.HAEdit', "icon": 'version', "needsIcon": true }
            ]
        },
        listeners: {
            //返回前一界面
            Back: function (list) {
                util.redirectTo(this.backUrl, "back", {});
                this.reset();
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
    menuTapButton: function (url, name) {
        util.redirectTo(url, "", {
            parentUrl: "QST.HouseManage.Housekeeping.List",
            url: config.url + '/Housekeeping/Add'
        });
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params) {
            this.backUrl = params.parentUrl;
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        //加载头部菜单信息
        this.add(this.getHeader());
    },
    //头部菜单信息(private)
    getHeader: function () {
        var me = this;
        if (!this._homeHeaderBar) {
            this._homeHeaderBar = Ext.create("app.user.NavigationBar", {
                title: me._title,
                docked: 'top'
            });
        }
        return this._homeHeaderBar;
    }
})