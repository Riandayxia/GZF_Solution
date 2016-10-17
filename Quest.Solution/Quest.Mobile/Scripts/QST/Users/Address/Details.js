/**-----------------------------------------------------------------
* @explanation:地址管理详细信息
* @created：XS
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Users.Address.Details', {
    extend: 'app.user.NTemplate',
    xtype: 'users_address_details',
    config: {
        title: config.str.AddressDetails,
        listMenu: [
         { text: config.str.Update, action: 'Update' },
         { text: config.str.Delete, action: 'Delete', cls: 'menusDel' }
        ],
        tpl: new Ext.XTemplate(
         '<div  class="table-d">',
            '<table>',
               '<tr ><th >收货人</th></tr>',
               '<tr><td>{Receiver}</td></tr>',
               '<tr ><th >联系电话</th></tr>',
               '<tr><td>{Mobile}</td></tr>',
               '<tr ><th >所在小区</th></tr>',
               '<tr><td>{TheCell}</td></tr>',
               '<tr ><th >详细地址</th></tr>',
               '<tr><td>{DetailedAddress}</td></tr>',
            '</table>',
            '</div>'
       ),
        listeners: {
            //更新数据事件
            Update: function (but, view, record) {
                this.getStructureMenu().hide();
                var data = this.data
                util.redirectTo("QST.Users.Address.Edit", "", {
                    parentUrl: "QST.Users.Address.List",
                    data: data,
                    url: config.url + '/Address/Update'
                });
            },
            //删除数据事件
            Delete: function (but, view, record) {
                var me = this;
                this.getStructureMenu().hide();
                util.DoDelete({
                    url: config.url + '/Address/Delete',
                    params: { ids: record.Id },
                    success: function (response) {
                        util.redirectTo(view.backUrl, "back", {});
                    }
                });
            },
            More: function (but, view) {
                this.setStructureMenu(this.config.listMenu);
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        var me = this;
        if (params.parentUrl) {
            me.backUrl = params.parentUrl;
        }
        if (params.data) {
            this.data = params.data;
            this.setTplData(params.data);
        }
    },
    //子界面到此界面时加载
    overViewResult: function (params) {
        this.backUrl = params.parentUrl;
        if (params.data) {
            this.setTplData(this.data);
        }
    }
})