/**-----------------------------------------------------------------
* @explanation:未处理的报事展示界面
* @created：HYF
* @create time：2016/10/9
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.NewsPaper.SList', {
    extend: 'QST.Property.NewsPaper.List',
    xtype: 'property_newspaper_slist',
    config: {
        isPage: false,
        addBut: false,
        addCls: 'action',
        moreBut: false,
        rootProperty: "data",
        cls: 'ux_list',
        search: true,//是否添加查询
        ckId: 'Id',  //设置数据主键(可配置)
        title: '未处理报事',
        dataUrl: config.url + '/NewsPaper/GetStare',
        modelArray: ['Id', 'NType', 'Address', 'Contacts', 'Phone', 'Title', 'Content', 'Status', 'Accessory', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
        listeners: {

            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back", {});
            },
            //单击查看详细信息
            itemsingletap: function (list, index, target, record, e, eOpts) {
                if (!this._tapHold) {
                    var record = util.copyObjects(record.data);
                    util.redirectTo("QST.Property.NewsPaper.Details", "", { parentUrl: "QST.Property.NewsPaper.List", data: record });
                }
            },
            //查询
            Search: function (field, list) {
                var search = field.getValue();
                //设置查询参数
                this.getStore().setParams({ search: search });
                this.getStore().loadPage(1);
            },
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
        this.getStore().load();
    },

})