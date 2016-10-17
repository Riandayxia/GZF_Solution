/*
* 判断
*/
Ext.define("BPM.WFModel.Line.Filter", {
    extend: 'Ext.panel.Panel',
    xtype: 'line_filter',
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    },
    // 初始化渲染
    afterRender: function () {
        var me = this;
        items = [{
            xtype: 'panel',
            layout: 'hbox',
            items: [
                { xtype: 'label', text: me.fieldText + ':', width: me.textWidth, style: 'text-align:right;padding:5px' },
                {
                    xtype: 'panel',
                    id: 'filter_root',
                    //defaults: {
                    //    labelAlign: 'right',
                    //    labelWidth: 90,
                    //},
                    items: [{
                        xtype: 'panel',
                        layout: {
                            type: 'table',
                            columns: 4
                        },
                        items: [
                            {
                                xtype: 'combo', width: 60, margin: '0 0 0 3px', name: 'G_Op',
                                displayField: 'text', valueField: 'value', value: 'and',
                                store: Ext.create('Ext.data.Store', {
                                    fields: ['text', 'value'],
                                    data: [
                                        { value: 'and', text: "并且" },
                                        { value: 'or', text: "或者" }
                                    ]
                                })
                            }, {
                                xtype: 'button',
                                text: '添加分组',
                                margin: '0 0 6 10',
                                listeners: {
                                    click: function (but) {
                                        var items = but.ownerCt.ownerCt;
                                        this.up("line_filter").addGroup('filter_root', items);
                                    }
                                }
                            }, {
                                xtype: 'button',
                                text: '添加条件',
                                margin: '0 0 6 10',
                                listeners: {
                                    click: function (but) {
                                        this.up("line_filter").addWhile('filter_root');
                                    }
                                }
                            }, {
                                xtype: 'button',
                                text: '获取数据',
                                margin: '0 0 6 10',
                                listeners: {
                                    click: function (but) {
                                        this.up("line_filter").getData();
                                    }
                                }
                            }
                        ]
                    }]
                }
            ]
        }]
        me.add(items);
        me.callParent(arguments);
    },
    addWhile: function (id, ct) {
        var me = this;
        var filter = me.down('[id="' + id + '"]');
        var items = [];
        //var zValue = ct.items.keys[ct.items.length - 1];
        //var start, stop;
        //start = zValue.indexOf('_') + 1;
        //stop = zValue.length;
        //var value = zValue.substring(start, stop);
        //var number = parseInt(value);
        //var rId = 'rule_1';
        //if (number) {
        //    rId = 'rule_' + ct.items.length;
        //}

        var whiles = [{
            xtype: 'panel', style: { 'background': '#f0f0f0' },
            layout: {
                type: 'table',
                columns: 4
            },
            items: [
                { xtype: 'hiddenfield', name: 'Type', width: 100, value: 'String', value: 'string' },
                { xtype: 'textfield', name: 'Field', width: 150, padding: 1, value: 'zd1' },
                { xtype: 'textfield', name: 'Op', width: 90, padding: '0 3', value: 'equal' },
                { xtype: 'textfield', name: 'Value', width: 150, padding: 1, value: '2' },
                {
                    xtype: 'button', text: '删除', margin: '0 0 6 10',
                    listeners: {
                        click: function (but) {
                            but.ownerCt.destroy();
                        }
                    }
                }]
        }]
        items = whiles;
        if (id == 'filter_root') {
            items = [];
            items.push({
                xtype: 'fieldset', margin: 0, border: 0,
                items: whiles
            });
        }
        filter.add(items);
    },
    addGroup: function (id, ct) {
        var me = this;
        var filter = me.down('[id="' + id + '"]');
        var items = [];
        var zValue = ct.items.keys[ct.items.length - 1];
        var start, stop;
        start = zValue.indexOf('_') + 1;
        stop = zValue.length;
        var value = zValue.substring(start, stop);
        var number = parseInt(value);
        var fId, rId;
        if (number) {
            fId = 'field_' + ct.items.length;
            rId = 'rule_' + ct.items.length;
        } else {
            fId = 'field_1'; rId = 'rule_1';
        }
        var groups = [
            {
                xtype: 'fieldset', border: 0, margin: 0,
                layout: {
                    type: 'table',
                    columns: 4,
                },
                items: [
                    {
                        xtype: 'combo', width: 60, margin: '0 0 0 3px', name: 'G_Op',
                        displayField: 'text', valueField: 'value', value: 'and',
                        store: Ext.create('Ext.data.Store', {
                            fields: ['text', 'value'],
                            data: [
                                { value: 'and', text: "并且" },
                                { value: 'or', text: "或者" }
                            ]
                        })
                        //}, {
                        //    xtype: 'button', text: '添加分组', margin: '0 0 3 10',
                        //    listeners: {
                        //        click: function (but) {
                        //            var items = but.ownerCt.ownerCt;
                        //            this.up("line_filter").addGroup(fId, items);
                        //        }
                        //    }
                    }, {
                        xtype: 'button', text: '添加条件', margin: '0 0 3 10',
                        listeners: {
                            click: function (but) {
                                this.up("line_filter").addWhile(rId);
                            }
                        }
                    }, {
                        xtype: 'button', text: '删除', margin: '0 0 6 10',
                        listeners: {
                            click: function (but) {
                                Ext.getCmp(fId).destroy();
                            }
                        }
                    }
                ]
            },
            {
                xtype: 'form', id: rId, margin: 0, border: 0,
                items: [{
                    xtype: 'panel', style: { 'background': '#f0f0f0' },
                    layout: {
                        type: 'table',
                        columns: 4
                    },
                    items: [
                        { xtype: 'hiddenfield', name: 'Type', width: 100, value: 'String', value: 'string' },
                        { xtype: 'textfield', name: 'Field', width: 150, padding: 1, value: 'zd1' },
                        { xtype: 'textfield', name: 'Op', width: 90, padding: '0 3', value: 'equal' },
                        { xtype: 'textfield', name: 'Value', width: 150, padding: 1, value: '2' },
                        {
                            xtype: 'button', text: '删除', margin: '0 0 6 10',
                            listeners: {
                                click: function (but) {
                                    but.ownerCt.destroy();
                                }
                            }
                        }]
                }]
            }
        ]
        items = groups;
        if (id == 'filter_root') {
            items = [];
            items.push({
                xtype: 'fieldset', id: fId, padding: 5, style: { 'background-color': '#f0f0f0' },
                items: groups
            });
        }
        filter.add(items);
    },
    getData: function () {
        var me = this;
        //var from = me.down('form');
        //var data = from.getValues();
        var f = me.down('[id="filter_root"]');
        var op = f.down('[name="G_Op"]').getValue();
        var groups = [];
        Ext.Array.forEach(f.items.items, function (item) {
            var from = item.down('form');
            if (from) {
                var data = from.getValues();
                var g_op = item.down('[name="G_Op"]');
                var opv = g_op.getValue();
                var rules = [];
                if (Ext.typeOf(data.Field) == 'array') {
                    Ext.Array.forEach(data.Field, function (rule, index) {
                        rules.push({
                            Type: data.Type[index],
                            Field: data.Field[index],
                            Op: data.Op[index],
                            Value: data.Value[index],
                        });
                    })
                } else {
                    rules.push(data);
                }
                groups.push({ Rules: rules, Op: opv });
            }
        });
        var gData = { Groups: groups, Op: op };
        return gData;
    },
    setData: function (data) {
        var me = this;

        var filter = me.down('[id="filter_root"]');
        //filter.down('').destroy();
        var items = [{
            xtype: 'panel',
            layout: {
                type: 'table',
                columns: 4
            },
            //items: [
            //    {
            //        xtype: 'combo', width: 60, margin: '0 0 0 3px', name: 'G_Op',
            //        displayField: 'text', valueField: 'value', value: 'and',
            //        store: Ext.create('Ext.data.Store', {
            //            fields: ['text', 'value'],
            //            data: [
            //                { value: 'and', text: "并且" },
            //                { value: 'or', text: "或者" }
            //            ]
            //        })
            //    }, {
            //        xtype: 'button',
            //        text: '添加分组',
            //        margin: '0 0 6 10',
            //        listeners: {
            //            click: function (but) {
            //                var items = but.ownerCt.ownerCt;
            //                this.up("line_filter").addGroup('filter_root', items);
            //            }
            //        }
            //    }, {
            //        xtype: 'button',
            //        text: '添加条件',
            //        margin: '0 0 6 10',
            //        listeners: {
            //            click: function (but) {
            //                this.up("line_filter").addWhile('filter_root');
            //            }
            //        }
            //    }, {
            //        xtype: 'button',
            //        text: '获取数据',
            //        margin: '0 0 6 10',
            //        listeners: {
            //            click: function (but) {
            //                this.up("line_filter").getData();
            //            }
            //        }
            //    }
            //]
        }];
        function SetRules(rules) {
            var pItems = [];
            if (rules) {
                Ext.Array.forEach(rules, function (item) {
                    pItems.push({ xtype: 'hiddenfield', name: 'Type', width: 100, value: item.Type });
                    pItems.push({ xtype: 'textfield', name: 'Field', width: 150, padding: 1, value: 'test' });
                    pItems.push({ xtype: 'textfield', name: 'Op', width: 90, padding: '0 3', value: item.Op });
                    pItems.push({ xtype: 'textfield', name: 'Value', width: 150, padding: 1, value: item.Value });
                    pItems.push({
                        xtype: 'button', text: '删除', margin: '0 0 6 10',
                        listeners: {
                            click: function (but) {
                                but.ownerCt.destroy();
                            }
                        }
                    });
                });
            }
            return pItems;
        }

        function SetGroups(groups) {
            if (groups) {
                Ext.Array.forEach(groups, function (item, index) {
                    var fId = 'field_' + index;
                    var rId = 'rule_' + index;
                    items.push({
                        xtype: 'fieldset', id: fId, padding: 5, style: { 'background-color': '#f0f0f0' },
                        items: [{
                            xtype: 'fieldset', border: 0, margin: 0,
                            layout: {
                                type: 'table',
                                columns: 4,
                            },
                            items: [
                                {
                                    xtype: 'combo', width: 60, margin: '0 0 0 3px', name: 'G_Op',
                                    displayField: 'text', valueField: 'value', value: 'and',
                                    store: Ext.create('Ext.data.Store', {
                                        fields: ['text', 'value'],
                                        data: [
                                            { value: 'and', text: "并且" },
                                            { value: 'or', text: "或者" }
                                        ]
                                    })
                                }, {
                                    xtype: 'button', text: '添加条件', margin: '0 0 3 10',
                                    listeners: {
                                        click: function (but) {
                                            this.up("line_filter").addWhile(rId);
                                        }
                                    }
                                }, {
                                    xtype: 'button', text: '删除', margin: '0 0 6 10',
                                    listeners: {
                                        click: function (but) {
                                            Ext.getCmp(fId).destroy();
                                        }
                                    }
                                }
                            ]
                        }, {
                            xtype: 'form', id: rId, margin: 0, border: 0,
                            items: [{
                                xtype: 'panel', style: { 'background': '#f0f0f0' },
                                layout: {
                                    type: 'table',
                                    columns: 4
                                },
                                items: SetRules(item.Rules)
                            }]
                        }]
                    });
                });
            }
        }
        //SetGroups(data.Groups);

        filter.add(items);
    }
});