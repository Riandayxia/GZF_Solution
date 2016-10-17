/*
数据表
*/
Ext.define('BPM.CDTable.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'compiler_cdtable_grid',
    stateId: 'compiler_cdtable_grid',
    controllerName: 'DBTable',
    al: true,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'CDTable/GetAll',
    modelArray: ['Id', 'CreatedTime', 'LastUpdatedTime', 'Name', 'ByName', 'Desc', 'Creator', 'Sort'],
    columns: [
        { text: '唯一标识', dataIndex: 'Id', flex: 1, minWidth: 0, hidden: true },
        { text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, minWidth: 0, hidden: true },
        { text: '数据库表名', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '别名', dataIndex: 'ByName', flex: 1, minWidth: 0 },
        { text: '序号', dataIndex: 'Sort', flex: 1, minWidth: 0, hidden: true },
        { text: '创建者', dataIndex: 'Creator', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreatedTime', flex: 1, minWidth: 0 },
        { text: '描述', dataIndex: 'Desc', flex: 1, minWidth: 0 }
    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'CDTable/Add',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'CDTable/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]

    }, {
        text: '删除',
        serveUrl: 'CDTable/Delete',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        text: '刷新',
        iconCls: 'icon_refresh',
        action: 'refresh_Data',
        type: [1, 2]
    }, {
        text: '动态编译',
        serveUrl: 'CDTable/Auto',
        iconCls: 'icon_bullseye',
        action: 'auto_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'CDTable/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    //监听器
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
        },
        // 刷新
        refresh_Data: function (but, record) {
            this.refresh_Data(but, record, this);
        },
        //动态编译
        auto_Data: function (but, record) {
            this.auto_Data(but, record, this);
        }
    },
    // 添加
    add_Data: function (but, record, grid) {
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.CDTable.Edit', hideFields: grid.typeFields, config: {
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
            winTitle: but.text, winWidth: 660, win: 'BPM.CDTable.Edit', hideFields: me.typeFields, config: {
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
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择删除数据");
            return;
        }

        util.doDelete({
            url: but.menuObj.serveUrl,//提交地址
            params: { ids: selected.Id },//参数
            success: function (result) {
                if (result) {
                    grid.store.load();
                }
            }
        });
    },
    // 查询
    search_Data: function (but, record) { },
    // 刷新
    refresh_Data: function (but, record) {
        var me = this;
        me.store.load();
    },
    //动态编译
    auto_Data: function (but, record) {
        util.request({
            url: but.menuObj.serveUrl,
            async: false,
            success: function (result) {
            }
        });
    }
});

