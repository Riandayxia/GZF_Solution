Ext.define('QST.Personal.Bottom', {
    extend: 'Ext.Container',
    xtype: 'personal_bottom',
    alternateClassName: 'personal_bottom',
    config: {
        layout: 'fit',
        height: 500,
        items: [{
            xtype: 'list',
            cls: 'list_wf',
            ullscreen: true,
            scrollable: {
                //注意横向竖向模式的配置，不能配错  
                direction: 'vertical',
                //隐藏滚动条样式  
                indicators: false
            },
            itemTpl: Ext.create('Ext.XTemplate',
                '<div class="menuList" style="<tpl if="needsIcon">background-color:#fff</tpl>">',
                     '<tpl if="needsIcon"><img width="{[this.getWidth(values.iconW)]}" height="{[this.getHeight(values.iconH)]}" style="margin: 5px 10px;" src="resources/images/set/{icon}.png" align="absmiddle" /></tpl>',
                     '<font style="margin:2px 0 10px 0">{name} </font>',
                     '<tpl if="needsIcon"><img width="6" height="15" src="resources/images/Arrow.png" style="display: inline; float: right; margin:18px"/></tpl>',
                 '</div>',
                 {
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
                 }
             ),
            data: [
                { "name": "我的", "icon": 'version', "needsIcon": false },
                { "name": "我的工单", url: 'QST.Property.NewsPaper.Layout', "icon": 'login', "needsIcon": true },
                { "name": "我的投资", url: 'SH.Main.ChangePassword', "icon": 'login', "needsIcon": true },
                { "name": "我的评论", url: 'SH.Main.ChangePassword', "icon": 'login', "needsIcon": true },
                { "name": "我的投诉", url: 'QST.Property.Complaints.List', "icon": 'login', "needsIcon": true },
                { "name": "其他", "icon": 'version', "needsIcon": false },
                { "name": "房产绑定", url: 'SH.App.Systems.Help.List', "icon": 'login', "needsIcon": true },
                { "name": "账号管理", url: 'SH.Main.UserFeedback', "icon": 'login', "needsIcon": true },
                { "name": "用户反馈", url: 'SH.Main.Version', "icon": 'login', "needsIcon": true }
            ],
            listeners: {

                //点击事件
                itemsingletap: function (list, index, target, record, e, eOpts) {

                    if (record.get('needsIcon')) {
                        list.up("personal_bottom").menuTapButton(record.raw.url)
                    }
                }
            }
        }]
    },
    // 菜单点击事件处理
    menuTapButton: function (url, name) {
        util.redirectTo(url, "", { parentUrl: "QST.Main.Layout", name: name });
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
})