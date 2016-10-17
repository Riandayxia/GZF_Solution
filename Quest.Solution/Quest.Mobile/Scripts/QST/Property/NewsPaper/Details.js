/**-----------------------------------------------------------------
* @explanation:借款详细信息
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.NewsPaper.Details', {
    extend: 'app.user.NTemplate',
    xtype: 'property_newspaper_details',
    config: {
        title: "报事详细",
        listMenu: [
            //{ text: config.str.Insert, action: 'Process' },
            { text: config.str.Update, action: 'Update' },
            { text: config.str.Delete, action: 'Delete', cls: 'menusDel' }
        ],
        tpl: new Ext.XTemplate(
            '<div  class="table-d">',
                '<table >',
                    '<tr><th>报事类型</th></tr><tr><td>{NType}</td></tr>',
                    '<tr><th>报事地址</th></tr><tr><td>{Address}</td></tr>',
                    '<tr><th>联系人</th></tr><tr><td>{Contacts}</td></tr>',
                    '<tr><th>联系电话</th></tr><tr><td>{Phone}</td></tr>',
                    '<tr><th>报事标题</th></tr><tr><td>{Title}</td></tr>',
                    '<tr><th>报事内容</th></tr><tr><td >{Content}</td></tr>',
                    //'<tr><th>状态</td></tr><tr><td >{[SHUtil.WFPro(values.State)]}</td></tr>',
                    //'<tr><th>附件</th></tr><tr><td>{[SHUtil.ShowImg(values.Accessory)]}</td></tr>',
                '</table>',
            '</div>'
        ),
        listeners: {
            Update: function (but, view, record) {
                this.getStructureMenu().hide();
                var data = this.data;
                util.redirectTo("QST.Property.NewsPaper.Edit", "",
                    {
                        parentUrl: "QST.Property.NewsPaper.List",
                        data: data,
                        url: config.url + '/NewsPaper/Update',
                    });
            },
            Process: function (but, view, record) {
                this.getStructureMenu().hide();
                function RunWF() {
                    view.Approvals();
                }
                SHUtil.IsCofing(this.data.Id, "QST.Property.NewsPaper.Details", RunWF, "Borrow");
            },
            Delete: function (but, view, record) {
                var me = this;
                this.getStructureMenu().hide();
                util.DoDelete({
                    url: config.url + '/NewsPaper/Delete',
                    params: { ids: this.data.Id },
                    success: function (response) {
                        util.redirectTo(view.backUrl, "back", {});
                    }
                });
            },// 更多功能
            More: function (but, view) {
                this.setStructureMenu(this.config.listMenu);
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        if (params) {
            if (params.parentUrl) {
                this.backUrl = params.parentUrl;
            }
            //if (params.IsEvent) {
            //    this.IsEvent = params.IsEvent;
            //} else {
            //    this.IsEvent = "";
            //}
            this.getHomeHeader().down("button[action='More']").show()
            if (params.data) {
                this.data = params.data
                ////判断状态
                //if ((this.data.State == 1 || this.data.State == 5 || this.data.State == 6) && this.IsEvent == true) {
                 
                //} else {
                //   
                //}
                this.setTplData(this.data);
            }
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        this.callParent(arguments);
        me.set_Listener();
    },
    // 设置事件
    set_Listener: function () {
        var me = this;
     
    },
    //子界面
    overViewResult: function (params) {
        if (params.data) {
            this.data = params.data
            this.setTplData(params.data);
        }
    }
})