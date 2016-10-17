/**-----------------------------------------------------------------
* @explanation:物业费账户编辑界面
* @created：HYF
* @create time：2016/10/15
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.PAccount.Edit', {
    extend: 'app.user.NForm',
    xtype: 'property_paccount_edit',
    config: {
        title: "新增账户",
        subUrl: config.url + '/PAccount/Add',
        defaults: {
            labelWidth: '30%'
        },
        items: [{
            xtype: 'fieldset',
            items: [{
                label: '小区名称',
                xtype: 'textfield',
                name: 'Name',
                placeHolder: '请输入小区名称（必填）'
            }, {
                label: '选择楼栋',
                xtype: 'textfield',
                name: 'Loudong'
            },
            {
                label: '选择门牌号',
                xtype: 'textfield',
                name: 'SerialNumber'
            }, {
                label: '缴费类型',
                xtype: 'hiddenfield',
                name: 'PType',
                value: "物业费"
            }, {
                xtype: 'hiddenfield',
                name: 'Id'
            },  {
                label: '是否删除',
                xtype: 'hiddenfield',
                name: 'IsDeleted'
            },{
                label: '创建时间',
                xtype: 'hiddenfield',
                name: 'CreatedTime',
                
            }, {
                label: '修改时间',
                xtype: 'hiddenfield',
                name: 'LastUpdatedTime'

            }, {
                xtype: 'label',
                html: '<div class="edetdiv">同意<span class="span">《智慧公租自助缴费协议》</span><div>'

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
            ////设置缴费类型值
            //this.down("hiddenfield[name='PType']").setValue(params.name);
            //设置头部标题
            this._headerBar.setTitle(params.name);
        }
       
    }
})