/**-----------------------------------------------------------------
* @explanation:加载服务编辑界面
* @created：XS
* @create time：2016/10/11
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.HouseManage.Housekeeping.CWEdit', {
    extend: 'app.user.NForm',
    xtype: 'houseManage_housekeeping_cwedit',
    config: {
        title: config.str.CareWorkers,
        defaults: {
            labelWidth: '20%'
        },
        items: [{
            xtype: 'fieldset',
            items: [
                { label: '唯一标识', name: 'Id', xtype: 'hiddenfield' },
                { label: '联系人', name: 'Contacts', xtype: 'textfield', allowBlank: true, placeHolder: '请填写联系人' },
                { label: '联系电话', name: 'Phone', xtype: 'textfield', matcher: /^1[3|4|5|8][0-9]{9}$/, message: '手机号码格式错误！', placeHolder: '请填写联系电话', allowBlank: true },
                { label: '服务地址', name: 'Address', xtype: 'textareafield', placeHolder: '请填写服务地址', allowBlank: true },
                { label: '服务价格', name: 'ServicePrice', xtype: 'textfield', value: '100元/天起', readOnly: true },
                { label: '服务类型', name: 'Type', xtype: 'hiddenfield', value: "10002003" },
                { label: '服务时长', name: 'Duration', xtype: 'textfield', placeHolder: '请选择服务时长', allowBlank: true },
                { label: '特殊要求', name: 'Content', xtype: 'textareafield', placeHolder: '告诉我们' },
                { label: '是否删除', name: 'IsDeleted', xtype: 'hiddenfield' },
                { label: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                { label: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
            ]
        }, {

            xtype: 'panel',
            layout: 'hbox',
            defaults: { flex: 1 },
            items: [
                {
                    html: '同意<span class="dl", style="color: #00BBFF ">' + config.str.Houseprotocol + '</span>',
                    style: {
                        'font-size': '1.4em;',
                        'text-align': 'left',
                        'margin': '10px 10px 5px 20px',
                        'color': ' #666',
                    }
                }
            ]

        }],
        listeners: {
            //返回前一界面
            Back: function (list) {
                util.redirectTo(this.backUrl, "back", {});
                this.reset();
            },
            //保存成功
            success: function () {
                util.redirectTo(this.backUrl, "back", { editSuccess: true });
                this.reset();
            }
        }
    },
    //初始化
    constructor: function (config) {
        var me = this;
        me.callParent(arguments);

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
                this.data = params.data;
                this.setValues(params.data);
            }
        }
    }
})