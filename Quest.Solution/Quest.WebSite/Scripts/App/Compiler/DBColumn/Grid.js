/*
数据列
*/
Ext.define('Compiler.DBColumn.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'compiler_dbcolumn_grid',
    stateId: 'compiler_dbcolumn_grid',
    controllerName: 'DBColumn',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'DBColumn/GetAll',
    modelArray: ['Id', 'CreatedTime', 'LastUpdatedTime', 'Name', 'TableId', 'Text', 'DBType', 'Desc'],
    columns: [
        { text: '唯一标识', dataIndex: 'Id', flex: 1, minWidth: 0, hidden: true },
        { text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, minWidth: 0, hidden: true },
        { text: '字段名称', dataIndex: 'Name', flex: 1, minWidth: 0 },
        { text: '字段文本', dataIndex: 'Text', flex: 1, minWidth: 0 },
        { text: '字段类型', dataIndex: 'DBType', flex: 1, minWidth: 0 },
        { text: '创建时间', dataIndex: 'CreatedTime', flex: 1, minWidth: 0 },
        { text: '描述', dataIndex: 'Desc', flex: 1, minWidth: 0 }
    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'DBColumn/Add',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'DBColumn/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]
    }, {
        text: '删除',
        serveUrl: 'DBColumn/Delete',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'DBColumn/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    // 时间监听器
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
        var me = this;
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'Compiler.DBColumn.Edit', hideFields: me.typeFields, config: {
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
            winTitle: but.text, winWidth: 660, win: 'Compiler.DBColumn.Edit', hideFields: me.typeFields, config: {
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
    search_Data: function (but, record, grid) { }
});

