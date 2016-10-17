/**-----------------------------------------------------------------
* @explanation:物业费缴费编辑界面
* @created：HYF
* @create time：2016/10/9 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.Payment.Edit', {
    extend: 'app.user.NForm',
    xtype: 'property_payment_edit',
    config: {
        title: "",
        subUrl: config.url + '/Payment/Add',
        defaults: {
            labelWidth: '30%'
        },
        items: [{
            xtype: 'fieldset',
            items: [
            {
                label: '缴费户号',
                xtype: 'textfield',
                name: 'SerialNumber',
                placeHolder: '请输入户号（必填）'
            }, {
                label: '户号',
                xtype: 'hiddenfield',
                name: 'SerialName',
                placeHolder: '请输入户号（必填）'
            },
             {
                 label: '缴费单位',
                xtype: 'textfield',
                name: 'PaymentUnit',
                placeHolder: '请输入缴费单位（必填）'
            }, {
                label: '缴费金额',
                xtype: 'numberfield',
                name: 'Amount',
                allowBlank: true,
                placeHolder: '请输入缴费金额（必填）'
            }, {
                label: '缴费类型',
                xtype: 'hiddenfield',
                name: 'PType',
                value: "物业费"
            }, {
                xtype: 'hiddenfield',
                name: 'Id'
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
                label: '提交人信息',
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
        if (params.name) {
            this.name = params.name;
            //设置缴费类型值
            this.down("hiddenfield[name='PType']").setValue(params.name);
            //设置头部标题
            this._headerBar.setTitle(params.name);
        }
       
    }
})