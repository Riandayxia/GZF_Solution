/**-----------------------------------------------------------------
* @explanation:缴费历史展示界面
* @created：HYF
* @create time：2016/10/11
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.PAccount.HList', {
    extend: 'app.user.NSimpleList',
    xtype: 'property_paccount_hlist',
    config: {
        isPage: false,
        addBut: false,
        addCls: 'action',
        moreBut: false,
        rootProperty: "data",
        cls: 'ux_list',
        search: true,//是否添加查询
        ckId: 'Id',  //设置数据主键(可配置)
        title: '缴费历史',
        dataUrl: config.url + '/PAccount/GetAll',
        modelArray: ['Id', 'SerialNumber', 'SName', 'PaymentUnit', 'PType', 'Amount', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
        itemTpl: Ext.create('Ext.XTemplate',
           '<div class="container">',
               '<div class="header"><h1>{PType}<h1></div>',
               '<div class="header2">',
                   '<div class="ctent" style="width:33%;"><li class="title">缴费类型</li><li><span class="_ctent">{PType}</span></li></div>',
                   '<div class="ctent" style="width:33%;"><li class="title">缴费金额</li><li><span class="_ctent">{Amount}</span></li></div>',
                   '<div class="ctent" style="width:33%;"><li class="title">缴费时间</li><li><span class="_ctent">{CreatedTime}</span></li></div>',
               '</div>',
               '<div class="footer"></div>',
           '</div>'),
        listeners: {

            //返回上一级
            Back: function (but, list) {
                util.redirectTo(this.backUrl, "back", {});
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
        this.getStore().load();
    },

})