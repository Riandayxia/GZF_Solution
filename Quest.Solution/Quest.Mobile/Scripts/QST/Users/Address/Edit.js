/**-----------------------------------------------------------------
* @explanation:地址管理编辑界面
* @created：XS
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Users.Address.Edit', {
    extend: 'app.user.NForm',
    xtype: 'users_address_edit',
    config: {
        title: config.str.AddressEdit,
        defaults: {
            labelWidth: '20%'
        },
        items: [{
            xtype: 'fieldset',
            items: [
                { label: '编号', name: 'Id', xtype: 'hiddenfield' },
                { label: '用户Id', name: 'UsersId', xtype: 'hiddenfield' },
                { label: '关联Id', name: 'AskfoleaveId', xtype: 'hiddenfield' },
                { label: '收货人', name: 'Receiver', xtype: 'textfield', allowBlank: true, placeHolder: '请填写收货人（必填）' },
                { label: '联系电话', name: 'Mobile', placeHolder: '请输入电话号码（必填）', xtype: 'numberfield', matcher: /^1[3|4|5|8][0-9]{9}$/, message: '手机号码格式错误！', allowBlank: true },
                { label: '所在小区', name: 'TheCell', xtype: 'textfield', allowBlank: true, placeHolder: '请填写所在小区（必填）' },
                { label: '详细地址', name: 'DetailedAddress', xtype: 'textareafield', allowBlank: true, placeHolder: '请填写详细地址（必填）' },
                { label: '设为默认地址', name: 'IsDefault', xtype: 'togglefield',  value: '1' },
                { label: '是否删除', name: 'IsDeleted', xtype: 'hiddenfield' },
                { label: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                { label: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },

            ],
        }],
        listeners: {
            //返回前一界面
            Back: function () {
                util.redirectTo(this.backUrl, "back");
            },
            //保存成功
            success: function () {
                util.redirectTo(this.backUrl, "back");
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        this.reset();
        if (params) {
            if (params.parentUrl) {
                this.backUrl = params.parentUrl;
            }
            if (params.url) {
                this.setSubUrl(params.url);
            }
            if (params.data) {
                this.setValues(params.data);
            }
        }
    }
})