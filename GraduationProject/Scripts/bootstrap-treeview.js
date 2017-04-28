/* =========================================================
 * bootstrap-treeview.js v1.0.0
 * =========================================================
 * Copyright 2013 Jonathan Miles 
 * Project URL : http://www.jondmiles.com/bootstrap-treeview
 *	
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================= */

; (function ($, window, document, undefined) {

    /*global jQuery, console*/

    'use strict';

    var pluginName = 'treeview';

    var Tree = function (element, options) {
        this.$element = $(element);
        this._element = element;
        this._elementId = this._element.id;
        this._styleId = this._elementId + '-style';
       
        this.tree = [];
        this.nodes = [];
        //this.selectedNode = {};
        
        this.callback = options.callback;
       
        //this._setSelectedNode({ id: "id6", text: "Parent 4", nodeId: 5 });
        this._init(options);
        this.clickTargetEvent();

    };

    Tree.defaults = {

        injectStyle: true,

        levels: 2,
        expandIcon: 'glyphicon glyphicon-plus',
        collapseIcon: 'glyphicon glyphicon-minus',
        emptyIcon: 'glyphicon',
        nodeIcon: 'glyphicon glyphicon-stop',

        color: undefined, // '#000000',
        backColor: undefined, // '#FFFFFF',
        borderColor: undefined, // '#dddddd',
        onhoverColor: '#f3f3f3',
        selectedColor: undefined,
        selectedBackColor: '#f3f3f3',

        enableLinks: false,
        highlightSelected: true,
        showBorder: true,
        showTags: false,

        // Event handler for when a node is selected
        onNodeSelected: undefined,
        // callback
        callback: null,
        ajaxObjj: {
            type: 'get',
            url: '/Department/GetSelectDeptString',
            contentType: "application/json",
            data: {
                appcode: 'OA',
                logintoken: $.cookie('OA_LoginToken')
            }
        },
        height:300
    };

    Tree.prototype = {

        remove: function () {

            this._destroy();
            $.removeData(this, 'plugin_' + pluginName);
            $('#' + this._styleId).remove();
        },

        _destroy: function () {

            if (this.initialized) {
                this.$wrapper.remove();
                this.$wrapper = null;

                // Switch off events
                this._unsubscribeEvents();
            }

            // Reset initialized flag
            this.initialized = false;
        },
        _createDOM: function (options) {
           
            this.$treeDrop = $('<div id="treeDrop" style="position:fixed;left:0;right:0;width:100%;height:100%;top:0;bottom:0;margin:auto;opacity:0;filter:alpha(opacity=0);z-index:9998;"></div>');
            this.targetBtn = $('<span style="position:absolute;right:0;top:0;height:31px;width:31px;display:block;" class="_target-has-btn"></span>');
            this.$element.wrap('<div class="section-name-wrapper"></div>');
            this.$sectionWrapper = this.$element.parent();
            this.$sectionWrapper.prepend(this.targetBtn);

            if(typeof options.clickTarget == 'undefined'){
                this.clickTarget = $('<input class="section-name form-control" />');
            } else {
                this.clickTarget = $(options.clickTarget);
            }
            //this.clickTarget = $('<input class="section-name form-control" />');
            this.clickTarget.css('cursor', 'pointer');
            this.clickTarget.focus(function () {
                $(this).blur();
            });
            this.$sectionWrapper.prepend(this.clickTarget);

           
            //this.$sectionWrapper = $('<div class="section-name-wrapper"></div>').append(this.clickTarget).append(this.targetBtn).append();
        },
        _init: function (options) {
            var that = this;
            this._createDOM(options);
            this.$element.css({
                top: '124%',
                display: 'none',
                position: 'absolute',
                left: 0,
                width:'100%',
                zIndex: 9999
            });
            
            this.$element.addClass('single-tree');
            //this.clickTarget = $(options.clickTarget);
            //if (options.targetHasBtn) {
            //    this.targetBtn = $('<span style="position:absolute;right:0;top:0;height:31px;width:31px;display:block;" class="_target-has-btn"></span>').insertAfter(this.clickTarget);
                
               
            //};
            //var clickTargetWidth = this.clickTarget.outerWidth();
            // this.$element.width('100%');
            if (options.data) {
                if (typeof options.data === 'string') {
                    options.data = $.parseJSON(options.data);
                }
                this.tree = $.extend(true, [], options.data);
                delete options.data;
               
            } else {
                var ajax = $.extend({}, options.ajax, Tree.defaults.ajaxObjj);
                if (typeof options.url !== 'undefined') {
                    ajax.url = options.url;
                }
                ajax.success = function (ajaxData) {
                   
                    if (ajaxData != null && ajaxData!="") {
                        var tree = $.parseJSON(ajaxData);
                        if (tree instanceof Array) {
                            that.tree = tree;
                        }
                        else {
                            that.tree = [tree];
                        }
                        
                    }
                    else {
                        that.tree = [];
                    }
                    
                  
                    doInit.apply(that);
                }
                //����ajax
              
                $.ajax(ajax);
                return;
            }
            function doInit() {
                
                this.options = $.extend({}, Tree.defaults, options);
                if (typeof options.url !== 'undefined') {
                    this.options.ajaxObjj.url = options.url;
                }
               // this.$element.height(this.options.height).attr('data-height', this.options.height);
                this._setInitialLevels(this.tree, 0);
                this._destroy();
                this._subscribeEvents();
                this._render();
               
                selectDefaultItem(this, this.options);
              
                //selectDefaultItem(this,this.options);
            }
            doInit.apply(this);
        },
        
        clickTargetEvent: function () {
          
            var that = this;
            var type = this.clickTarget.is('input') ? 'focusin' : 'click';
            function clickTargetEvent($node) {
              
                if ($("#treeDrop").length <= 0) {
                    $('<div id="treeDrop" style="position:fixed;left:0;right:0;width:100%;height:100%;top:0;bottom:0;margin:auto;opacity:0;filter:alpha(opacity=0);background:#fff;z-index:9998;"></div>').insertBefore(that.$element);
                    //var left = $node.offset().left;
                    //var top = $node.offset().top + $node.height();
                   
                    var bottom = $(window).height() - that.$sectionWrapper.offset().top - that.$sectionWrapper.height();
                    var top = $(window).height() - bottom - that.$sectionWrapper.height();
                    if(bottom > top){
                        that.$element.css({ top: '100%', bottom: 'auto' });
                    } else {
                        that.$element.css({ bottom: '100%', top: 'auto' });
                    }
                    that.$element.hide();
                    that.$element.slideDown('fast');
                    that.targetBtn && that.targetBtn.addClass('_targetOpened');
                    //that.clickTarget.addClass('_targetOpened');
                    $('#treeDrop').bind('click', function () {
                        $(this).remove();
                        that.$element.slideUp('fast');
                        that.targetBtn && that.targetBtn.removeClass('_targetOpened');
                        // that.clickTarget.removeClass('_targetOpened');

                    });
                }
            }
            this.clickTarget.bind(type, function () {
                clickTargetEvent($(this));
                
            });
           
            this.targetBtn && this.targetBtn.click(function () {
                clickTargetEvent(that.clickTarget);
                //$(this).toggleClass('_targetOpened');
                
                
            });
        },
        _unsubscribeEvents: function () {

            this.$element.off('click');
        },

        _subscribeEvents: function () {

            this._unsubscribeEvents();

            this.$element.bind('click', $.proxy(this._clickHandler, this));

            if (typeof (this.options.onNodeSelected) === 'function') {
                this.$element.on('nodeSelected', this.options.onNodeSelected);
            }
        },

        _clickHandler: function (event, node) {
          
            var that = this;
            if (!this.options.enableLinks) { event.preventDefault(); }

            var target = $(event.target),
				classList = target.attr('class') ? target.attr('class').split(' ') : [],
				node = this._findNode(target);
           
            var isSpan = target.hasClass('click-expand') || target.hasClass('click-collapse');
            if (isSpan) {//(classList.indexOf('click-expand') != -1) ||
               // (classList.indexOf('click-collapse') != -1)
                // Expand or collapse node by toggling child node visibility
                this._toggleNodes(node);
                this._render();
            }
            else if (node) {
                
                var li = $(event.target).closest('a');
                var itemHover = $('.itemHover');
                itemHover.removeClass('.itemHover');
                li.addClass('.itemHover');
                
                var text = li.length > 0 ? li.text() : '';
                var id = li.attr('dataid');
               
                
                this.$element.slideUp();
                that.targetBtn &&  that.targetBtn.removeClass('_targetOpened');
                $('#treeDrop').remove();
                if (this._isSelectable(node)) {
                    this._setSelectedNode(node);
                } else {
                    this._toggleNodes(node);
                    this._render();
                }
                this.clickTarget.val(text);
                //this.clickTarget.text(text);
                this.callback && this.callback(li, id,  text);
            }
        },

        // Looks up the DOM for the closest parent list item to retrieve the 
        // data attribute nodeid, which is used to lookup the node in the flattened structure.
        _findNode: function (target) {

            var nodeId = target.closest('a.list-group-item').attr('data-nodeid'),
				node = this.nodes[nodeId];

            if (!node) {
                console.log('Error: node does not exist');
            }
            return node;
        },

        // Actually triggers the nodeSelected event
        _triggerNodeSelectedEvent: function (node) {
           
            this.$element.trigger('nodeSelected', [$.extend(true, {}, node)]);
        },

        // Handles selecting and unselecting of nodes, 
        // as well as determining whether or not to trigger the nodeSelected event
        _setSelectedNode: function (node) {
          
            if (!node) { return; }
           
            if (node === this.selectedNode) {
                this.selectedNode = {};
            }
            else {
               
                this._triggerNodeSelectedEvent(this.selectedNode = node);
            }
            
            this._render();
        },

        // On initialization recurses the entire tree structure 
        // setting expanded / collapsed states based on initial levels
        _setInitialLevels: function (nodes, level) {

            if (!nodes) { return; }
            level += 1;
            var self = this;
            $.each(nodes, function addNodes(id, node) {

                if (level >= self.options.levels) {
                    self._toggleNodes(node);
                }

                // Need to traverse both nodes and _nodes to ensure 
                // all levels collapsed beyond levels
                var nodes = node.nodes ? node.nodes : node._nodes ? node._nodes : undefined;
                if (nodes) {
                    return self._setInitialLevels(nodes, level);
                }
            });
        },

        // Toggle renaming nodes -> _nodes, _nodes -> nodes
        // to simulate expanding or collapsing a node.
        _toggleNodes: function (node) {

            if (!node.nodes && !node._nodes) {
                return;
            }

            if (node.nodes) {
                node._nodes = node.nodes;
                delete node.nodes;
            }
            else {
                node.nodes = node._nodes;
                delete node._nodes;
            }
        },

        // Returns true if the node is selectable in the tree
        _isSelectable: function (node) {
            return node.selectable !== false;
        },

        _render: function () {
            
            var self = this;

            if (!self.initialized) {

                // Setup first time only components
                self.$element.addClass(pluginName);
                self.$wrapper = $(self._template.list);

                self._injectStyle();

                self.initialized = true;
            }

            self.$element.empty().append(self.$wrapper.empty());


            // Build tree
            self.nodes = [];
            self._buildTree(self.tree, 0);
        },

        // Starting from the root node, and recursing down the 
        // structure we build the tree one node at a time
        _buildTree: function (nodes, level) {
           
            if (!nodes) { return; }
            level += 1;

            var self = this;

           
            $.each(nodes, function addNodes(id, node) {

                node.nodeId = self.nodes.length;
                self.nodes.push(node);              
                var treeItem = $(self._template.item)
					.addClass('node-' + self._elementId)
					.addClass((node === self.selectedNode) ? 'node-selected' : '')
					.attr('data-nodeid', node.nodeId)
                    .addClass('level-' + level)
					.attr('style', self._buildStyleOverride(node))
                    .addClass('hoverItem');

                // Add indent/spacer to mimic tree structure
                for (var i = 0; i < (level - 1) ; i++) {
                    treeItem.append(self._template.indent);
                }

                // Add expand, collapse or empty spacer icons 
                // to facilitate tree structure navigation
                if (node._nodes) {
                    treeItem
						.append($(self._template.expandCollapseIcon)
							.addClass('click-expand')
							.addClass(self.options.expandIcon)
						);
                }
                else if (node.nodes) {
                    treeItem
						.append($(self._template.expandCollapseIcon)
							.addClass('click-collapse')
							.addClass(self.options.collapseIcon)
						);
                }
                else {
                    treeItem
						.append($(self._template.expandCollapseIcon)
							.addClass(self.options.emptyIcon)
						);
                }

                // Add node icon
                //treeItem
				//	.append($(self._template.icon)
				//		.addClass(node.icon ? node.icon : self.options.nodeIcon)
				//	);

                // Add text
                if (self.options.enableLinks) {
                    // Add hyperlink
                    treeItem
						.append($(self._template.link)
							.attr('href', node.href)
							.append(node.text)
						);
                }
                else {
                    // otherwise just text
                    treeItem
						.append(node.text).attr('dataid', node.id);
                }

                // Add tags as badges
                if (self.options.showTags && node.tags) {
                    $.each(node.tags, function addTag(id, tag) {
                        treeItem
							.append($(self._template.badge)
								.append(tag)
							);
                    });
                }

                // Add item to the tree
                self.$wrapper.append(treeItem);

                // Recursively add child ndoes
               
                if (node.nodes) {
                   
                    return self._buildTree(node.nodes, level);
                }
            });
        },

        // Define any node level style override for
        // 1. selectedNode
        // 2. node|data assigned color overrides
        _buildStyleOverride: function (node) {
           
            var style = '';
            if (this.options.highlightSelected && (node === this.selectedNode)) {
                style += 'color:' + this.options.selectedColor + ';';
            }
            else if (node.color) {
                style += 'color:' + node.color + ';';
            }

            if (this.options.highlightSelected && (node === this.selectedNode)) {
                style += 'background-color:' + this.options.selectedBackColor + ';';
            }
            else if (node.backColor) {
                style += 'background-color:' + node.backColor + ';';
            }

            return style;
        },

        // Add inline style into head 
        _injectStyle: function () {

            if (this.options.injectStyle && !document.getElementById(this._styleId)) {
                $('<style type="text/css" id="' + this._styleId + '"> ' + this._buildStyle() + ' </style>').appendTo('head');
            }
        },

        // Construct trees style based on user options
        _buildStyle: function () {

            var style = '.node-' + this._elementId + '{';
            if (this.options.color) {
                style += 'color:' + this.options.color + ';';
            }
            if (this.options.backColor) {
                style += 'background-color:' + this.options.backColor + ';';
            }
            if (!this.options.showBorder) {
                style += 'border:none;';
            }
            else if (this.options.borderColor) {
                style += 'border:1px solid ' + this.options.borderColor + ';';
            }
            style += '}';

            if (this.options.onhoverColor) {
                style += '.node-' + this._elementId + ':hover{' +
				'background-color:' + this.options.onhoverColor + ';' +
				'}';
            }

            return this._css + style;
        },

        _template: {
            list: '<ul class="list-group"></ul>',
            item: '<a href="javascript:;" class="list-group-item"></a>',
            indent: '<span class="indent"></span>',
            expandCollapseIcon: '<span class="expand-collapse" style="margin-right:5px;"></span>',
            icon: '<span class="icon"></span>',
            link: '<a href="#" style="color:inherit;"></a>',
            badge: '<span class="badge"></span>'
        },
        // get parentids
        _css: '.list-group-item{cursor:pointer;}span.indent{margin-left:10px;margin-right:10px}span.expand-collapse{width:1rem;height:1rem}span.icon{margin-left:10px;margin-right:5px}'
        // _css: '.list-group-item{cursor:pointer;}.list-group-item:hover{background-color:#f5f5f5;}span.indent{margin-left:10px;margin-right:10px}span.icon{margin-right:5px}'
    };

    var logError = function (message) {
        if (window.console) {
            window.console.error(message);
        }
    };
    function GetData(selectItem, itemId, nodeitem) {
        
        var whetherEnd = false;
        if (nodeitem.id == itemId) {
            selectItem.unshift(nodeitem);
            whetherEnd = true;
        }
        
        var nodes = nodeitem.nodes || nodeitem._nodes;
        if (!whetherEnd && nodes != null) {
            $.each(nodes, function (index, item) {
               
                GetData(selectItem,itemId, item);
            });
        }
        else {
            return;
        }
    }
    // Prevent against multiple instantiations,
    // handle updates and method calls
    $.fn[pluginName] = function (options, args) {

        

        return this.each(function () {
            var self = $.data(this, 'plugin_' + pluginName);
            if (typeof options === 'string') {
                if (!self) {
                    logError('Not initialized, can not call method : ' + options);
                }
                else if (!$.isFunction(self[options]) || options.charAt(0) === '_') {
                    logError('No such method : ' + options);
                }
                else {
                    if (typeof args === 'string') {
                        args = [args];
                    }
                    self[options].apply(self, args);
                }
            }
            else {
                
                if (!self) {
                   
                    var tree = new Tree(this, $.extend(true, {}, options));
                    
                    //var parentId = options.data.length > 0 ? options.data[0].parentId : "0";
                   
                    
                    
                    // selectDefaultItem(tree, options);
                    
                    


                    $.data(this, 'plugin_' + pluginName, tree);
                   
                }
                else {
                    self._init(options);
                   
                }
            }
        });
    };
    function selectDefaultItem(tree, options) {
        var parentId = tree.tree.length > 0 ? tree.tree[0].parentId : "0";
        if (tree.tree.length > 0) {
            if (options.selectedItem && $.trim(options.selectedItem) != parentId) {

                var selectItem = [];
                while (selectItem.length <= 0 || $.trim(selectItem[0].parentId) != parentId) {
                    var selectId = '';
                    if (selectItem.length <= 0) {
                        selectId = options.selectedItem;
                    } else {
                        selectId = selectItem[0].parentId;
                    }
                    $.each(tree.tree, function (index, item) {

                        GetData(selectItem, selectId, item)
                    });
                }

                $.each(selectItem, function (index, item) {
                    if (index == selectItem.length - 1) {
                        return;
                    }
                    var dataidItem = tree.$element.find('[dataid="' + item.id + '"]');
                    //$(".expand-collapse", $("#" + item.id)).click();
                    //console.log($(".expand-collapse", $("#" + item.id)));
                    $(".expand-collapse.glyphicon-plus", dataidItem).click();
                    //setTimeout(function () {
                    //    $(".expand-collapse", $("#" + item.id)).click();
                    //}, 0);
                });

                
            }
            $('a[dataid="' + options.selectedItem + '"]', tree.$element).click();
        }
        
    }
})(jQuery, window, document);