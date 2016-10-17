/**-----------------------------------------------------------------
* @explanation:流程显示
* @created：rainday
* @create time：2016-08-09 10:11
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 流程显示
*/
Ext.define("BPM.WFModel.Display", {
    extend: 'Ext.Panel',
    xtype: 'bpm_wfmodel_display',
    requires: [
        'UX.WorkFlow.Designer',
    ],
    items: [{
        xtype: 'wfdesigner',
        layout: {
            type: 'vbox'
        },
    }],
    // 事件监听器
    listeners: {
        render: function () {
            var me = this;
            me.loadFlow();
        }
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.myWF = me.down('wfdesigner').WF;
        me.drawArr();
    },
    // 初始化
    initComponent: function () {
        var me = this;
        me.callParent(arguments);

        me.wf_width = 108; //步骤宽度
        me.wf_height = 50; //步骤高度
        me.wf_steps = []; //步骤数组
        me.wf_texts = []; //文本数组
        me.wf_conns = []; //连线数组
        me.wf_connColor = "#898a89"; //连线的常规颜色
        me.wf_designer = false; //是否是设计模式(查看流程图时不帮定双击事件）
        me.tempArrPath = []; //临时连线
        me.mouseX = 0;
        me.mouseY = 0;
    },
    // 加载流程
    loadFlow: function () {
        var me = this;
        util.request({
            url: 'WFModel/GetById',
            params: { folwId: me.folwId },//参数
            method: 'POST',
            async: true,
            success: function (result) {
                if (result.success) {
                    me.wf_json = Ext.decode(result.data.DesignJSON);
                    var steps = me.wf_json.Steps;
                    steps.forEach(function (item, index) {
                        var position = { x: parseInt(item.Position_x), y: parseInt(item.Position_y), width: me.wf_width, height: me.wf_height };
                        me.addStep(position, item.Name, item.Id, false, item.Type);
                    });

                    var lines = me.wf_json.Lines;
                    lines.forEach(function (item, index) {
                        me.connObj({ Id: item.Id, obj1: me.myWF.getById(item.FromID), obj2: me.myWF.getById(item.ToID) }, false, item.Text);
                    });
                    console.dir(result.data);
                }
            }
        });
    },
    //添加步骤
    addStep: function (position, text, id, addToJSON, type1, bordercolor, bgcolor) {
        var me = this;

        var x = position.x;
        var y = position.y;
        var width = position.width;
        var height = position.height;
        text = text || '新步骤';
        var rect = me.myWF.rect(x, y, width, height, me.myWFect);
        rect.attr({ "fill": '#efeff0', "stroke": '#587aa9', "stroke-width": 1.4, "cursor": "default" });
        rect.id = id;
        rect.type1 = type1 ? type1 : "normal";

        me.wf_steps.push(rect);

        var text2 = text.length > 8 ? text.substr(0, 7) + "..." : text;
        var text1 = me.myWF.text(x + 52, y + 25, text2);
        text1.attr({ "font-size": "12px" });
        if (text.length > 8) text1.attr({ "title": text });
        text1.id = "text_" + id;
        text1.type1 = "text";
        me.wf_texts.push(text1);

    },
    //连接对象
    connObj: function (obj, addToJSON, title) {
        var me = this;
        if (addToJSON == undefined || addToJSON == null) addToJSON = true;

        if (me.isLine(obj)) {
            var newline = me.myWF.drawArr(obj);
            me.wf_conns.push(newline);
            if (addToJSON) {
                var line = {};
                line.Id = obj.Id;
                line.Text = title || "";
                line.FromID = obj.obj1.id;
                line.ToID = obj.obj2.id;
                me.addLine(line);
            }
            else {
                me.setLineText(obj.Id, title);
            }
        }
    },
    //判断一个节点与另一个节点之间是否可以连线
    isLine: function (obj) {
        var me = this;
        if (!obj || !obj.obj1 || !obj.obj2) {
            return false;
        }
        if (obj.obj1 === obj.obj2) {
            return false;
        }
        if (!me.isStepObj(obj.obj1) || !me.isStepObj(obj.obj2)) {
            return false;
        }
        if (me.wf_conns) {
            me.wf_conns.forEach(function (item, index) {
                if (obj.obj1 === obj.obj2 || (item.obj1 === obj.obj1 && item.obj2 === obj.obj2)) {
                    return false;
                }
            });
        }
        return true;
    },
    //判断一个对象是否是步骤对象
    isStepObj: function (obj) {
        return obj && obj.type1 && (obj.type1.toString() == "normal" || obj.type1.toString() == "subflow" || obj.type1.toString() == "judge");
    },
    //添加线
    addLine: function (line) {
        var me = this;
        if (!line || !line.FromID || !line.ToID) return;
        if (!me.wf_json.Lines) me.wf_json.Lines = [];
        var isup = false;
        for (var i = 0; i < me.wf_json.Lines.length; i++) {
            if (me.wf_json.Lines[i].Id == line.Id) {
                me.wf_json.Lines[i] = line;
                isup = true;
            }
        }
        if (!isup) {
            me.wf_json.Lines.push(line);
        }
        me.setLineText(line.Id, line.Text);
    },
    //随着节点位置的改变动态改变箭头
    drawArr: function () {
        var me = this;

        Raphael.fn.drawArr = function (obj) {
            if (!obj || !obj.obj1) {
                return;
            }
            if (!obj.obj2) {
                var point1 = me.getStartEnd(obj.obj1, obj.obj2);
                var path2 = me.getArr(point1.start.x, point1.start.y, me.mouseX, me.mouseY, 7);
                for (var i = 0; i < me.tempArrPath.length; i++) {
                    me.tempArrPath[i].arrPath.remove();
                }
                me.tempArrPath = [];
                obj.arrPath = this.path(path2);
                obj.arrPath.attr({ "stroke-width": 2, "stroke": me.wf_connColor, "fill": me.wf_connColor });
                me.tempArrPath.push(obj);
                return;
            }

            var point = me.getStartEnd(obj.obj1, obj.obj2);
            var path1 = me.getArr(point.start.x, point.start.y, point.end.x, point.end.y, 7);
            try {
                if (obj.arrPath) {
                    obj.arrPath.attr({ path: path1 });
                }
                else {
                    obj.arrPath = this.path(path1);
                    obj.arrPath.attr({ "stroke-width": 1.7, "stroke": me.wf_connColor, "fill": me.wf_connColor, "x": point.start.x, "y": point.start.y, "x1": point.end.x, "y1": point.end.y });
                    if (me.wf_designer) {
                        obj.arrPath.id = obj.Id;
                        obj.arrPath.fromid = obj.obj1.id;
                        obj.arrPath.toid = obj.obj2.id;
                    }
                }
            } catch (e) { }

            return obj;
        }
    },
    getStartEnd: function (obj1, obj2) {
        var bb1 = obj1 ? obj1.getBBox() : null;
        var bb2 = obj2 ? obj2.getBBox() : null;
        var p = [
                    { x: bb1.x + bb1.width / 2, y: bb1.y - 1 },
                    { x: bb1.x + bb1.width / 2, y: bb1.y + bb1.height + 1 },
                    { x: bb1.x - 1, y: bb1.y + bb1.height / 2 },
                    { x: bb1.x + bb1.width + 1, y: bb1.y + bb1.height / 2 },
                    bb2 ? { x: bb2.x + bb2.width / 2, y: bb2.y - 1 } : {},
                    bb2 ? { x: bb2.x + bb2.width / 2, y: bb2.y + bb2.height + 1 } : {},
                    bb2 ? { x: bb2.x - 1, y: bb2.y + bb2.height / 2 } : {},
                    bb2 ? { x: bb2.x + bb2.width + 1, y: bb2.y + bb2.height / 2 } : {}
        ];
        var d = {}, dis = [];
        for (var i = 0; i < 4; i++) {
            for (var j = 4; j < 8; j++) {
                var dx = Math.abs(p[i].x - p[j].x),
                            dy = Math.abs(p[i].y - p[j].y);
                if (
                        (i == j - 4) ||
                        (((i != 3 && j != 6) || p[i].x < p[j].x) &&
                        ((i != 2 && j != 7) || p[i].x > p[j].x) &&
                        ((i != 0 && j != 5) || p[i].y > p[j].y) &&
                        ((i != 1 && j != 4) || p[i].y < p[j].y))
                    ) {
                    dis.push(dx + dy);
                    d[dis[dis.length - 1]] = [i, j];
                }
            }
        }
        if (dis.length == 0) {
            var res = [0, 4];
        } else {
            res = d[Math.min.apply(Math, dis)];
        }
        var result = {};
        result.start = {};
        result.end = {};
        result.start.x = p[res[0]].x;
        result.start.y = p[res[0]].y;
        result.end.x = p[res[1]].x;
        result.end.y = p[res[1]].y;
        return result;
    },
    getArr: function (x1, y1, x2, y2, size) {
        var angle = Raphael.angle(x1, y1, x2, y2);
        var a45 = Raphael.rad(angle - 28);
        var a45m = Raphael.rad(angle + 28);
        var x2a = x2 + Math.cos(a45) * size;
        var y2a = y2 + Math.sin(a45) * size;
        var x2b = x2 + Math.cos(a45m) * size;
        var y2b = y2 + Math.sin(a45m) * size;
        return ["M", x1, y1, "L", x2, y2, "M", x2, y2, "L", x2b, y2b, "L", x2a, y2a, "z"].join(",");
    },
    // 设置线条内容
    setLineText: function (id, txt) {
        var me = this;
        var line;
        for (var i = 0; i < me.wf_conns.length; i++) {
            if (me.wf_conns[i].Id == id) {
                line = me.wf_conns[i];
                break;
            }
        }

        if (!line) {
            return;
        }
        var bbox = line.arrPath.getBBox();
        var txt_x = (bbox.x + bbox.x2) / 2;
        var txt_y = ((bbox.y + bbox.y2) / 2) - 10;

        var lineText = me.myWF.getById("line_" + id);
        if (lineText != null) {
            if (!txt) {
                lineText.remove();
            }
            else {
                lineText.attr("x", txt_x);
                lineText.attr("y", txt_y);
                lineText.attr("fill", '#066FBB');
                lineText.attr("font-size", '6');
                lineText.attr({ "text": txt || "" });
            }
            return;
        }
        if (txt) {
            var textObj = me.myWF.text(txt_x, txt_y, txt);
            textObj.attr("fill", '#066FBB');
            textObj.attr("font-size", '6');
            textObj.type1 = "line";
            textObj.id = "line_" + id;
            me.wf_texts.push(textObj);
        }
    }
});
