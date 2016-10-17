/**-----------------------------------------------------------------
* @explanation:停车费账户界面
* @created：HYF
* @create time：2016/10/15
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Property.PAccount.TEdit', {
    extend: 'app.user.NForm',
    xtype: 'property_paccount_tedit',
    config: {
        title: "新增账户",
        subUrl: config.url + '/PAccount/Add',
        defaults: {
            labelWidth: '30%'
        },
        items: [{
            xtype: 'fieldset',
            items: [
              {
                  label: '小区名称',
                  xtype: 'textfield',
                  name: 'Name'
              }, {
                  label: '车位编号',
                  xtype: 'textfield',
                  name: 'SerialNumber',
                  placeHolder: '请输入车位编号（必填）'
              }, {
                label: '缴费类型',
                xtype: 'hiddenfield',
                name: 'PType',
                value: "停车费"
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