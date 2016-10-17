/**-----------------------------------------------------------------
* @explanation:投诉建议信息编辑界面
* @created：HYF
* @create time：2016/10/9 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.NewsPaper.Edit', {
    extend: 'app.user.NForm',
    xtype: 'property_newspaper_edit',
    config: {
        title: "新增报事",
        subUrl: config.url + '/NewsPaper/Add',
        defaults: {
            labelWidth: '30%'
        },
        items: [{
            xtype: 'fieldset',
            items: [{
                label: '报事分类',
                xtype: 'selectfield',
                name: 'NType',
                options: [
                        { text: '类型一', value: '类型一' },
                        { text: '类型二', value: '类型二' },
                        { text: '类型三', value: '类型三' }
                ]
            },
            {
                label: '报事地址',
                xtype: 'textareafield',
                name: 'Address',
                placeHolder: '请输入报事地址'
            },
             {
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
                label: '报事标题',
                xtype: 'textfield',
                name: 'Title',
                placeHolder: '请输入报事标题'
            }, {
                label: '报事内容',
                xtype: 'textareafield',
                name: 'Content',
                placeHolder: '请输入报事内容'
            }, {
                xtype: 'hiddenfield',
                name: 'Id'
            }, {
                label: '处理状态',
                xtype: 'hiddenfield',
                name: 'Status',
                value:1
            }, {
                label: '附件',
                xtype: 'hiddenfield',
                name: 'Accessory'
            }, {
                label: '是否删除',
                xtype: 'hiddenfield',
                name: 'IsDeleted'
            }, {
                label: '创建时间',
                xtype: 'hiddenfield',
                name: 'CreatedTime',
                
            }, {
                label: '创建时间',
                xtype: 'hiddenfield',
                name: 'LastUpdatedTime'

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