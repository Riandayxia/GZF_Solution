/**-----------------------------------------------------------------
* @explanation:数据表信息列表
* @created：rainday
* @create time：2016-09-01 09:58
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.CDTable.RGrid', {
    extend: 'BPM.CDTable.Grid',
    xtype: 'bpm_cdtable_rgrid',
    al: true,
    initComponent: function () {
        var me = this;
        me.addFeatures([{
            text: '动态编译',
            serveUrl: 'CDTable/Auto',
            iconCls: 'icon_bullseye',
            action: 'auto_Data',
            type: [1, 2]
        }], true);
        this.callParent(arguments);
        // 点击事件
        me.addListener('auto_Data', function (but, record) {
            me.auto_Data(but, record, this);
        });
    },
    //动态编译
    auto_Data: function (but, record) {
        util.request({
            url: but.menuObj.serveUrl,
            async: false,
            success: function (result) {
                if (record.success) {
                    util.msgTip('编译成功');
                }
            }
        });
    },
    // 添加
    add_Data: function (but, record, grid) {
        var me = this;
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.CDTable.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.load_Data();
                    }
                }
            }
        }
        if (!grid.winEdit) {
            grid.winEdit = util.createWindow(object);
        }
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        // 打开窗体
        grid.winEdit.show();
    }
});
