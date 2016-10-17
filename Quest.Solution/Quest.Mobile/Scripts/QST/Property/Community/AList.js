/**-----------------------------------------------------------------
* @explanation:投诉建议信息展示界面
* @created：HYF
* @create time：2016/10/9
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Community.AList', {
    extend: 'app.user.NSimpleList',
    xtype: 'property_community_list',
    config: {
        addBut: false,
        addCls: 'action',
        moreBut: false,
        rootProperty: "data",
        cls: 'ux_list',
        ckId: 'Id',  //设置数据主键(可配置)
        title: '社区公告',
        dataUrl: config.url + '/Community/GetAll',
        modelArray: ['Id', 'Title', 'Content', 'ActivityTime', 'Text', 'ContentType', 'Publisher', 'ImageUrl', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
        //模板
        itemTpl: Ext.create('Ext.XTemplate',
            '<div class="container">',
                '<div class="header">',
                    ' <h1 ><span class="communityspan">公告</span> {Title}<h1><p class="communityp">{[SHUtil.FormatTime(values.ActivityTime)]}</p>',
                '</div>',
                '<div ><span  class="communityp">{[SHUtil.GetText(values.Text)]}</span></div>',
                '<div class="menu">{[SHUtil.GetImg(values.ImageUrl,values.Title)]}</div>',
                '<div ><span  class="communityp">点击查看详细</span></div>',
                '<div class="footer"></div>',
            '</div>'
            ),
        listeners: {

            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back", {});
            },
            //单击查看详细信息
            itemsingletap: function (list, index, target, record, e, eOpts) {
                if (!this._tapHold) {
                    var record = util.copyObjects(record.data);
                    util.redirectTo("QST.Property.Community.Details", "", { parentUrl: "QST.Property.Community.AList", data: record });
                }
            },
            //查询
            Search: function (field, list) {
                var search = field.getValue();
                //设置查询参数
                this.getStore().setParams({ search: search });
                this.getStore().loadPage(1);
            }
        }
    },
    //主界面到此界面时加载[List刷新时会默认加载此方法]
    rendering: function (params) {
        if (params) {
            this.backUrl = params.parentUrl;
        }
        //加载数据
        this.getStore().load();
    },
    //子界面返回到此界面时加载
    overViewResult: function (params) {
        //this.getStore().loadPage(1);
        this.getStore().load();
    }

})