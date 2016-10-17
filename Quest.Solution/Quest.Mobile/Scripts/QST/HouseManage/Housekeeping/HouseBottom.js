/**-----------------------------------------------------------------
* @explanation:家政服务界面
* @created：XS
* @create time：2016/10/11
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.HouseManage.Housekeeping.HouseBottom', {
    alternateClassName: 'housemanage_housekeeping_housebottom',
    extend: 'Ext.Container',
    xtype: 'housemanage_housekeeping_housebottom',
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
             '<div class="menuList" style="height:100px !important; <tpl if="needsIcon">background-color:#fff</tpl>">',
                '<tpl if="needsIcon" ><img width="{[this.getWidth(values.iconW)]}" height="{[this.getHeight(values.iconH)]}" style="margin: 25px 10px;" src="{icon}" align="absmiddle" /></tpl>',
                '<font style="margin:25px 0 10px 0">{name} </font>',
                '<tpl if="needsIcon"><img width="16" height="25" src="resources/images/Arrow.png" style="display: inline; float: right; margin:50px 10px 0 0;"/></tpl>',
            '</div>', {
                getWidth: function (iconW) {
                    if (!iconW) {
                        return 50;
                    }
                    if (iconW > 50) {
                        return iconW;
                    } else {
                        return 50;
                    }
                },
                getHeight: function (iconH) {
                    if (!iconH) {
                        return 50;
                    }
                    if (iconH > 50) {
                        return iconH;
                    } else {
                        return 50;
                    }
                }
            }),
            store: {
                fields: ['name', 'icon', 'needsIcon', 'url', 'style', 'iconW', 'iconH'],
                data: [
                    { "name": "钟点工", url: 'QST.HouseManage.Housekeeping.PJEdit', "icon": 'resources/images/HouseManage/Housekeeping/zhongdiangong@2x.png', "needsIcon": true },
                    { "name": "月嫂", url: 'QST.HouseManage.Housekeeping.YSEdit', "icon": 'resources/images/HouseManage/Housekeeping/yuesao@2x.png', "needsIcon": true },
                    { "name": "护工", url: 'QST.HouseManage.Housekeeping.CWEdit', "icon": 'resources/images/HouseManage/Housekeeping/hugong@2x.png', "needsIcon": true },
                    { "name": "住家阿姨", url: 'QST.HouseManage.Housekeeping.HAEdit', "icon": 'resources/images/HouseManage/Housekeeping/zhujiaayi@2x.png', "needsIcon": true },
                    { "name": "育儿嫂", url: 'QST.HouseManage.Housekeeping.PAEdit', "icon": 'resources/images/HouseManage/Housekeeping/yuersao@2x.png', "needsIcon": true }
                ]
            },
            listeners: {//点击事件
                itemsingletap: function (list, index, target, record, e, eOpts) {
                    if (record.get('needsIcon')) {
                        list.up("housemanage_housekeeping_housebottom").menuTapButton(record.get('url'))
                    }
                }
            }
        }]
    },
    // 菜单点击事件处理
    menuTapButton: function (url, name) {
        util.redirectTo(url, "", {
            parentUrl: "QST.HouseManage.Housekeeping.HouseList",
            url: config.url + '/Housekeeping/Add'
        });
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
})