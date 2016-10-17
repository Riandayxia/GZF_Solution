/**-----------------------------------------------------------------
* @explanation:投诉建议信息展示界面
* @created：HYF
* @create time：2016/10/9
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Community.List', {
    extend: 'app.user.NSimpleList',
    xtype: 'property_Community_list',
    config: {
        addBut: false,
        addCls: 'action',
        moreBut: false,
        rootProperty: "data",
        cls: 'ux_list',
        ckId: 'Id',  //设置数据主键(可配置)
        title: '社区活动',
        dataUrl: config.url + '/Community/GetAll',
        modelArray: ['Id', 'Title', 'Content', 'ActivityTime', 'Text', 'ContentType', 'ImageUrl', 'Publisher', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
        //模板
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="container">',
                '<div class="header">',
                    ' <h1>{Title}<h1><p class="communityp">{[SHUtil.FormatTime(values.ActivityTime)]}</p>',
                '</div>',
                '<div ><span  class="communityp">{[SHUtil.GetText(values.Text)]}</span></div>',
                '<div class="menu">{[SHUtil.GetImg(values.ImageUrl,values.Title)]}</div>',
                '<div ><span class="communityp">点击查看详细</span></div>',
                '<div class="footer"></div>',
            '</div>'),
        listeners: {
            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back");
            },
            //单击查看详细信息
            itemsingletap: function () {
                if (!this._tapHold) {
                    var record = this.getSelection()[0].data;
                    util.redirectTo("QST.Property.Community.Details", "", { parentUrl: "QST.Property.Community.List", data: record });
                }
            },
            //只要按键长按住就会触发，和按键是否离开没有关系
            itemtaphold: function (list, index, target, record, e) {
                //开始多选
                this.beginSimple();
            }
        }
    },
    // 加载数据
    loadData: function (cType) {
        this.getStore().setParams({ ContentType: cType });
        this.getStore().loadPage(1);
    },
 
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params)
            this.backUrl = params.parentUrl;
        this.getStore().loadPage(1);
    },
    //子界面返回到此界面时加载
    overViewResult: function (params) {
        //当编辑数据成功时加载数据
        if (params.editSuccess) {
            this.getStore().loadPage(1);
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
    }
})