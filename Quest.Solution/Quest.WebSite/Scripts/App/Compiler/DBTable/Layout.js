/// <reference path="../UDController/Auto/Edit.js" />
/*
数据表管理布局
*/
Ext.define('Compiler.DBTable.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'main_layout',
    layout: 'border',
    requires: [
        'Compiler.DBTable.Grid',
        'Compiler.DBTable.Tab',
    ],
    items: [{
        region: 'center', xtype: 'compiler_dbtable_grid', title: '数据表', collapsible: true, al: true
    }, {
        region: 'south', xtype: 'compiler_dbtable_tab',  height: '50%'
    }],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    },
    // 初始化
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
        me.set_Listener();
    },
    // 设置事件
    set_Listener: function () {
        var me = this;
        var tableGuid = me.down("compiler_dbtable_grid");
        var columnGuid = me.down("compiler_dbtable_tab").down("compiler_dbcolumn_grid");
        var controllerGuid = me.down("compiler_dbtable_tab").down("compiler_udcontroller_grid");
        // 点击事件
        tableGuid.addListener('itemclick', function (e, record) {
            // 数据表点击事件
            columnGuid.store.load({ params: { tableId: record.get("Id") } });
            // 控制器点击事件
            controllerGuid.store.load({ params: { tableId: record.get("Id") } });
        });
        //数据列表 添加 重写
        Ext.override(columnGuid, {
            add_Data: function (but, record, grid) {

                var parentItem = tableGuid.getSelectionTobject();
                if (parentItem != null) {
                    //获得 添加窗体对象
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
                    //给添加窗体赋值
                    form.setValues({ TableId: parentItem.Id });
                    // 打开窗体
                    grid.winEdit.show();
                } else {
                    Ext.Msg.alert("提示", "请选择数据表数据！");
                    return;
                }
            }
        });
        //控制器 添加 重写
        Ext.override(controllerGuid, {
            add_Data: function (but, record, grid) {

                var parentItem = tableGuid.getSelectionTobject();
                if (parentItem != null) {
                    //获得 添加窗体对象
                    var object = {
                        winTitle: but.text, winWidth: 800, winheight: 650, win: 'Compiler.UDController.Edit', hideFields: me.typeFields, config: {
                            saveUrl: but.menuObj.serveUrl,
                            onSaveSuccess: function (action) {
                                if (action.result.success) {
                                    grid.store.load({ params: { tableId: parentItem.Id } });
                                }
                            }
                        }
                    }
                    grid.winEdit = util.createWindow(object);
                    var form = grid.winEdit.down("form").getForm();
                    form.reset();
                    ////给添加窗体赋值
                    form.setValues({ TableId: parentItem.Id });
                    // 打开窗体
                    grid.winEdit.show();
                } else {
                    Ext.Msg.alert("提示", "请选择控制器数据！");
                    return;
                }
            },
            // 修改
            updata_Data: function (but, record, grid) {
                var me = this;
                //得到选中的数据
                var selected = grid.getSelectionTobject();
                var parentItem = tableGuid.getSelectionTobject();
                if (!selected) {
                    Ext.Msg.alert("提示", "请选择修改数据");
                    return;
                }
                //获得 添加窗体对象
                var object = {
                    winTitle: but.text, winWidth: 800, winheight: 650, win: 'Compiler.UDController.Edit', hideFields: me.typeFields, config: {
                        saveUrl: but.menuObj.serveUrl,
                        onSaveSuccess: function () {
                            grid.store.load({ params: { tableId: parentItem.Id} });
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
                var parentItem = tableGuid.getSelectionTobject();
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
                            grid.store.load({ params: { tableId: parentItem.Id } });
                        }
                    }
                });
            },
        });
    }
});
