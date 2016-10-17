/**-----------------------------------------------------------------
* @explanation:主界面头部滚动图片
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Main.HomeMiddleImg', {
    extend: 'Ext.Container',
    xtype: 'main_middle_img',
    config: {
        layout: 'vbox',
        items: [{
            xtype: 'panel',
            cls: 'home_msg',
            html: '<div  class="speaker"><div class="img"><img  fire="onDelete" src="resources/images/main/speaker2x.png"></div><div class="div">|&nbsp;&nbsp;&nbsp;停电通知：2016-10-10 8：00至12：00</div> </div>'
        }, {
            xtype: 'panel',
            height: 50,
            html: '<img  class="middle_img" fire="onDelete" src="resources/images/middle_img.png">'
        }, {
            xtype: 'panel',
            layout: 'hbox',
            height: 100,
            margin: '5px 0px',
            items: [{
                xtype: 'panel',
                flex: 1,
                html: '<img  class="middle_img_button" fire="onDelete" src="resources/images/middle_img_2.png">'
            }, {
                html: '',
                width: 5
            }, {
                xtype: 'panel',
                flex: 1,
                html: '<img  class="middle_img_button" fire="onDelete" src="resources/images/middle_img_3.png">'
            }]
        }]
    }
});
