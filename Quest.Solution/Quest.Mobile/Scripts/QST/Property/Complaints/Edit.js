/**-----------------------------------------------------------------
* @explanation:投诉建议信息编辑界面
* @created：HYF
* @create time：2016/10/9 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Complaints.Edit', {
    extend: 'app.user.NForm',
    xtype: 'property_complaints_edit',
    config: {
        title: "新增投诉建议",
        subUrl: config.url + '/Complaints/Add',
        defaults: {
            labelWidth: '30%'
        },
        items: [{
            xtype: 'fieldset',
            items: [
                {
                label: '类型',
                xtype: 'selectfield',
                name: 'CType',
                options: [
                       { text: '安保', value: '安保' },
                       { text: '环境', value: '环境' },
                       { text: '客服', value: '客服' },
                       { text: '维修', value: '维修' },
                       { text: '其他', value: '其他' }
                ]
            },
            {
                label: '投诉内容',
                xtype: 'textareafield',
                name: 'Content',
            },
            {
                label: '提交人信息',
                xtype: 'textfield',
                name: 'Submitter',
                placeHolder: '请输入提交人信息'
            }, {
                label: '联系人',
                xtype: 'textfield',
                name: 'Contacts',
                placeHolder: '请输入联系人'
            }, {
                label: '联系电话',
                xtype: 'textfield',
                name: 'Phone',
                placeHolder: '请输入联系电话'
            }, {
                label: '详细地址',
                xtype: 'textareafield',
                name: 'Address',
                placeHolder: '请输入详细地址'
            }, {
                xtype: 'hiddenfield',
                name: 'Id'
            }, {
                label: '是否删除',
                xtype: 'hiddenfield',
                name: 'IsDeleted'
            }, {
                label: '创建时间',
                xtype: 'hiddenfield',
                name: 'CreatedTime'
            }, {
                name: 'LastUpdatedTime',
                xtype: 'hiddenfield',
                label: '修改时间'

            }]
        }],
        listeners: {
            //返回前一界面
            Back: function () {
                util.redirectTo(this.backUrl, "back", {});
                this.reset();
            },
            //保存成功
            success: function (but, view) {
                util.redirectTo(this.backUrl, "back", { editSuccess: true });
                this.reset();
            }
        }
    },
    //主界面到此界面时加载
    rendering: function (params) {
        //清空原始数据
        this.reset();
        this.backUrl = params.parentUrl;
        if (params.url) {
            this.setSubUrl(params.url);
        }
        if (params.data) {
            this.data = params.data;
            this.setValues(params.data);
        } else {
            this.data = "";
        }
    }
})