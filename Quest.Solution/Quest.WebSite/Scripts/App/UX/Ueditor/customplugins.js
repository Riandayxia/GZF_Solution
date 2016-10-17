//新建表单
UE.plugins['formadd'] = function () {
    var me = this, thePlugins = 'formadd';
    me.commands[thePlugins] = {
        execCommand: function (name, edom, myUE) {
            var meEC = this;
            //获得 添加窗体对象
            var object = {
                winTitle: '表单编辑', winWidth: 450, win: 'BPM.WFForm.dialogs.Attribute', config: {
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var data = view.getValues();
                            if (!parent.formattributeJSON) {
                                parent.formattributeJSON = data;
                            } else {
                                Ext.apply(parent.formattributeJSON, data);
                            }
                            me.setContent('');
                        }
                    }
                }
            }
            if (!this.winEdit_Attribute)
                this.winEdit_Attribute = util.createWindow(object);

            var form = this.winEdit_Attribute.down("form").getForm();
            form.reset();

            // 打开窗体
            this.winEdit_Attribute.show();
        }
    };
};
//表单属性
UE.plugins['formattribute'] = function () {
    var me = this, thePlugins = 'formattribute';
    me.commands[thePlugins] = {
        execCommand: function (name, edom, myUE) {
            //获得 添加窗体对象
            var object = {
                winTitle: '表单编辑', winWidth: 400, win: 'BPM.WFForm.dialogs.Attribute', config: {
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var data = view.getValues();
                            if (!parent.formattributeJSON) {
                                parent.formattributeJSON = data;
                            } else {
                                Ext.apply(parent.formattributeJSON, data);
                            }
                        }
                    }
                }
            }

            var form = this.winEdit_Attribute.down("form").getForm();
            form.reset();
            //给添加窗体赋值
            form.setValues(parent.formattributeJSON);
            if (!this.winEdit_Attribute)
                this.winEdit_Attribute = util.createWindow(object);
            // 打开窗体
            this.winEdit_Attribute.show();
        }
    };
};
//保存表单
UE.plugins['formsave'] = function () {
    var me = this, thePlugins = 'formsave';
    me.commands[thePlugins] = {
        execCommand: function () {
            var data = {};
            Ext.apply(data, parent.formattributeJSON);
            Ext.apply(data, {
                DesignHtml: me.getContent()
            });
            util.request({
                url: 'WFForm/AddOrUpdate',
                async: false,
                params: data,
                success: function (result) {
                    util.msgTip(result.msg);
                }
            });
        }
    };
};
//打开表单
UE.plugins['formopen'] = function () {
    var me = this, thePlugins = 'formopen';
    me.commands[thePlugins] = {
        execCommand: function (name, edom, myUE) {
            var meEC = this;
            //获得 添加窗体对象
            var object = {
                winTitle: '表单列表', winWidth: 450, winHeight: 300, win: 'BPM.WFForm.dialogs.Open', config: {
                    // 时间监听器
                    listeners: {
                        itemdblclick: function (view, record, item, index, e, eOpts) {
                            view.up('window').hide();
                            var data = record.data;
                            parent.formattributeJSON = data;
                            me.setContent(data.DesignHtml);
                            // me.execCommand('insertHtml', data.Html);
                        }
                    }
                }
            }
            if (!this.winEdit_Open)
                this.winEdit_Open = util.createWindow(object);

            this.winEdit_Open.down('open_grid').store.load();
            // 打开窗体
            this.winEdit_Open.show();
        }
    };
};
//文本框
UE.plugins['formtext'] = function () {
    var me = this, thePlugins = 'formtext';
    me.commands[thePlugins] = {
        execCommand: function (name, edom, myUE) {
            var meEC = this;
            meEC.eDom = edom;
            if (!parent.formattributeJSON) {
                alert('未创建表单！');
                return;
            }
            var fromData = {};
            Ext.apply(fromData, parent.formattributeJSON);
            //获得 添加窗体对象
            var object = {
                winTitle: '文本框编辑', winWidth: 500, win: 'BPM.WFForm.dialogs.TextBox', config: {
                    fromData: fromData,
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var values = view.getValues();
                            var html = '<input type="text" id="' + values.BindFiled + '" type1="flow_text" name="' + values.BindFiled + '" value="文本框" ';
                            if (values.TextWidth && values.TextHeight) {
                                html += 'style="width:' + values.TextWidth + ';height:' + values.TextHeight + '" ';
                                html += 'width1="' + values.TextWidth + '" ';
                                html += 'height1="' + values.TextHeight + '" ';
                            } else {
                                html += 'style="width:120px; height:20px;" ';
                                html += 'width1="120px" ';
                                html += 'height1="20px" ';
                            }

                            if (values.Defaultvalue) {
                                html += 'defaultvalue="' + encodeURI(values.Defaultvalue) + '"  value="' + encodeURI(values.Defaultvalue) + '" ';
                            }
                            if (values.ValueType) {
                                html += 'valuetype="' + values.ValueType + '" ';
                            }
                            html += '/>';
                            if (meEC.eDom) {
                                UE.dom.domUtils.remove(meEC.eDom, false);
                            }
                            me.execCommand('insertHtml', html);
                        }
                    }
                }
            }
            if (!this.winEdit_TextBox)
                this.winEdit_TextBox = util.createWindow(object);

            var form = this.winEdit_TextBox.down("form").getForm();
            form.reset();

            if (this.eDom) {
                var aData = this.eDom.attributes;
                var data = {
                    BindFiled: aData.id ? aData.id.value : '',
                    Defaultvalue: aData.defaultvalue ? aData.defaultvalue.value : '文本框',
                    TextWidth: aData.width1 ? aData.width1.value : '120px',
                    TextHeight: aData.height1 ? aData.height1.value : '20px',
                    ValueType: aData.valuetype ? aData.valuetype.value : 'string'
                };

                //给添加窗体赋值
                form.setValues(data);
            }
            // 打开窗体
            this.winEdit_TextBox.show();

        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function () {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins, popup.anchorEl);
            this.hide();
        },
        _delete: function () {
            if (window.confirm('确认删除该控件吗？')) {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt) {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', '')) {
            var html = popup.formatHtml('<nobr>文本框: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html) {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else {
                popup.hide();
            }
        }
    });
};
//文本域
UE.plugins['formtextarea'] = function () {
    var me = this, thePlugins = 'formtextarea';
    me.commands[thePlugins] = {
        execCommand: function (name, edom, myUE) {
            var meEC = this;
            meEC.eDom = edom;
            if (!parent.formattributeJSON) {
                alert('未创建表单！');
                return;
            }
            var fromData = {};
            Ext.apply(fromData, parent.formattributeJSON);
            //获得 添加窗体对象
            var object = {
                winTitle: '文本域编辑', winWidth: 500, win: 'BPM.WFForm.dialogs.Textarea', config: {
                    fromData: fromData,
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var values = view.getValues();
                            var html = '<textarea id="' + values.BindFiled + '" type1="flow_textarea" name="' + values.BindFiled + '"';
                            if (values.TextWidth && values.TextHeight) {
                                html += 'style="width:' + values.TextWidth + ';height:' + values.TextHeight + '" ';
                                html += 'width1="' + values.TextWidth + '" ';
                                html += 'height1="' + values.TextHeight + '" ';
                            } else {
                                html += 'style="width:80%; height:60px;" ';
                                html += 'width1="80%" ';
                                html += 'height1="60px" ';
                            }
                            if (values.ValueType) {
                                html += 'valuetype="' + values.ValueType + '" ';
                            }
                            if (values.Defaultvalue) {
                                html += 'defaultvalue="' + encodeURI(values.Defaultvalue) + '" ';
                                html += '>' + encodeURI(values.Defaultvalue) + '</textarea>';
                            } else {
                                html += 'defaultvalue="文本域" ';
                                html += '>文本域</textarea>';
                            }
                            if (meEC.eDom) {
                                UE.dom.domUtils.remove(meEC.eDom, false);
                            }
                            me.execCommand('insertHtml', html);
                        }
                    }
                }
            }
            if (!this.winEdit_Textarea)
                this.winEdit_Textarea = util.createWindow(object);

            var form = this.winEdit_Textarea.down("form").getForm();
            form.reset();

            if (this.eDom) {
                var aData = this.eDom.attributes;
                var data = {
                    BindFiled: aData.id ? aData.id.value : '',
                    Defaultvalue: aData.defaultvalue ? aData.defaultvalue.value : '',
                    TextWidth: aData.width1 ? aData.width1.value : '80%', 
                    TextHeight:  aData.height1 ? aData.height1.value : '90px',
                    ValueType:  aData.valuetype ? aData.valuetype.value : ''
                };

                //给添加窗体赋值
                form.setValues(data);
            }
            // 打开窗体
            this.winEdit_Textarea.show();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function () {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins, popup.anchorEl);
            this.hide();
        },
        _delete: function () {
            if (window.confirm('确认删除该控件吗？')) {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt) {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/textarea/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', '')) {
            var html = popup.formatHtml('<nobr>文本域: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html) {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else {
                popup.hide();
            }
        }
    });
};