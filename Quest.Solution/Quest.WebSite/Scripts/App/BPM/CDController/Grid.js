/*
数据表
*/
Ext.define('BPM.CDController.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_cdcontroller_grid',
    stateId: 'bpm_cdcontroller_grid',
    controllerName: 'UDController',
    al: true,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'CDController/GetAll',
    modelArray: ['Id', 'CreatedTime', 'LastUpdatedTime', 'TableId', 'Code', 'Creator', 'Desc'],
    columns: [
        { text: '唯一标识', dataIndex: 'Id', flex: 1, minWidth: 0, hidden: true },
        { text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, minWidth: 0, hidden: true },
        { text: '代码', dataIndex: 'Code', flex: 1, minWidth: 0 },
        { text: '创建者', dataIndex: 'Creator', flex: 1, minWidth: 0, hidden: true },
        { text: '创建时间', dataIndex: 'CreatedTime', flex: 1, minWidth: 0, hidden: true },
        { text: '描述', dataIndex: 'Desc', flex: 1, minWidth: 0, hidden: true }
    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'CDController/Add',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'CDController/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]

    }, {
        text: '删除',
        serveUrl: 'CDController/Del',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'CDController/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    // 事件监听器
    listeners: {
        // 添加
        add_Data: function (but, record) {
            this.add_Data(but, record, this);
        },
        // 修改
        updata_Data: function (but, record) {
            this.updata_Data(but, record, this);
        },
        // 删除
        del_Data: function (but, record) {
            this.del_Data(but, record, this);
        },
        // 查询
        search_Data: function (but, record) {
            this.search_Data(but, record, this);
        }
    },
    // 添加
    add_Data: function (but, record, grid) {
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.CDController.Auto.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.store.load();
                    }
                }
            }
        }
        grid.winEdit = util.createWindow(object);
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        // 打开窗体
        grid.winEdit.show();
    },
    // 修改
    updata_Data: function (but, record, grid) {
        var me = this;
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择修改数据");
            return;
        }
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 800, winheight: 650, win: 'BPM.CDController.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function () {
                    grid.store.load();
                }
            }
        }
        grid.winEdit = util.createWindow(object);
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(selected);
        // 打开窗体
        grid.winEdit.show();
    },
    // 删除
    del_Data: function (but, record, grid) {
        var me = this;
        ////得到选中的数据
        //var selected = grid.getSelectionTobject();
        //if (!selected) {
        //    Ext.Msg.alert("提示", "请选择删除数据");
        //    return;
        //}
        //util.doDelete({
        //    url: but.menuObj.serveUrl,//提交地址
        //    params: { ids: selected.Id },//参数
        //    success: function (result) {
        //        if (result) {
        //            grid.store.load();
        //        }
        //    }
        //});
        util.request({
            url: 'CDController/Test',
            async: false,
            success: function (result) {
            }
        });
    },
    // 查询
    search_Data: function (but, record, grid) { }
});

