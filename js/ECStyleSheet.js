(function () {
  "use strict";

  var MOBILE_BREAKPOINT = 768;
  var TABLET_BREAKPOINT = 1024;
  var STYLE_TAG_ID = "ec-stylesheet-rules";
  var PROCESSED_ATTR = "data-ec-processed";
  var IS_BORDER_BOX_SET = false;

  var propertyMap = {
    padding: "padding",
    paddingTop: "padding-top",
    paddingRight: "padding-right",
    paddingBottom: "padding-bottom",
    paddingLeft: "padding-left",
    margin: "margin",
    marginTop: "margin-top",
    marginRight: "margin-right",
    marginBottom: "margin-bottom",
    marginLeft: "margin-left",
    width: "width",
    minWidth: "min-width",
    maxWidth: "max-width",
    height: "height",
    minHeight: "min-height",
    maxHeight: "max-height",
    boxSizing: "box-sizing",
    overflow: "overflow",
    overflowX: "overflow-x",
    overflowY: "overflow-y",

    display: "display",
    visibility: "visibility",
    opacity: "opacity",
    position: "position",
    top: "top",
    right: "right",
    bottom: "bottom",
    left: "left",
    zIndex: "z-index",
    float: "float",
    clear: "clear",

    flexDirection: "flex-direction",
    flexWrap: "flex-wrap",
    flexFlow: "flex-flow",
    justifyContent: "justify-content",
    alignItems: "align-items",
    alignContent: "align-content",
    alignSelf: "align-self",
    flex: "flex",
    flexGrow: "flex-grow",
    flexShrink: "flex-shrink",
    flexBasis: "flex-basis",
    order: "order",
    gap: "gap",
    rowGap: "row-gap",
    columnGap: "column-gap",

    gridTemplateColumns: "grid-template-columns",
    gridTemplateRows: "grid-template-rows",
    gridColumn: "grid-column",
    gridRow: "grid-row",
    gridArea: "grid-area",
    gridGap: "grid-gap",

    fontSize: "font-size",
    fontWeight: "font-weight",
    fontFamily: "font-family",
    fontStyle: "font-style",
    fontVariant: "font-variant",
    lineHeight: "line-height",
    letterSpacing: "letter-spacing",
    textAlign: "text-align",
    textDecoration: "text-decoration",
    textTransform: "text-transform",
    textOverflow: "text-overflow",
    whiteSpace: "white-space",
    wordBreak: "word-break",
    wordWrap: "word-wrap",
    color: "color",

    background: "background",
    backgroundColor: "background-color",
    backgroundImage: "background-image",
    backgroundSize: "background-size",
    backgroundPosition: "background-position",
    backgroundRepeat: "background-repeat",

    border: "border",
    borderTop: "border-top",
    borderRight: "border-right",
    borderBottom: "border-bottom",
    borderLeft: "border-left",
    borderWidth: "border-width",
    borderStyle: "border-style",
    borderColor: "border-color",
    borderRadius: "border-radius",
    borderTopLeftRadius: "border-top-left-radius",
    borderTopRightRadius: "border-top-right-radius",
    borderBottomLeftRadius: "border-bottom-left-radius",
    borderBottomRightRadius: "border-bottom-right-radius",
    outline: "outline",
    outlineColor: "outline-color",
    outlineWidth: "outline-width",
    outlineStyle: "outline-style",
    outlineOffset: "outline-offset",
    boxShadow: "box-shadow",

    transform: "transform",
    transformOrigin: "transform-origin",
    transition: "transition",
    animation: "animation",
    animationDuration: "animation-duration",
    animationTimingFunction: "animation-timing-function",
    animationDelay: "animation-delay",

    cursor: "cursor",
    pointerEvents: "pointer-events",
    userSelect: "user-select",
    resize: "resize",

    objectFit: "object-fit",
    objectPosition: "object-position",
    listStyle: "list-style",
    appearance: "appearance",
    content: "content",
    verticalAlign: "vertical-align",
    tableLayout: "table-layout",
    borderCollapse: "border-collapse",
    borderSpacing: "border-spacing"
  };

  function escapeClassName(name) {
    return name.replace(/[:\.\[\]\/!@#$%^&*()+=,<>?;'"{}|~`\\]/g, function (c) {
      return "\\" + c;
    });
  }

  function underscoresToSpaces(value) {
    return value.replace(/_/g, " ");
  }

  function camelToKebab(str) {
    return str.replace(/([A-Z])/g, function (match) {
      return "-" + match.toLowerCase();
    });
  }

  function resolveProperty(rawProp) {
    if (propertyMap[rawProp]) {
      return propertyMap[rawProp];
    }
    return camelToKebab(rawProp);
  }

  function parseClassName(token) {
    var parts = token.split(":");
    var isMobile = false;
    var isTablet = false;
    var pseudoClass = null;
    var declaration = null;

    if (parts.length === 1) {
      declaration = parts[0];
    } else if (parts.length === 2) {
      if (parts[0] === "mobile") {
        isMobile = true;
        declaration = parts[1];
      } else if (parts[0] === "tablet") {
        isTablet = true;
        declaration = parts[1];
      } else {
        pseudoClass = parts[0];
        declaration = parts[1];
      }
    } else if (parts.length === 3) {
      if (parts[0] === "mobile") {
        isMobile = true;
        pseudoClass = parts[1];
        declaration = parts[2];
      } else if (parts[0] === "tablet") {
        isTablet = true;
        pseudoClass = parts[1];
        declaration = parts[2];
      }
    }

    if (!declaration) {
      return null;
    }

    if (declaration === "eccard") {
      return {
        className: token,
        isMobile: isMobile,
        isTablet: isTablet,
        pseudoClass: pseudoClass,
        cssText: "background: var(--ec-bg, #ffffff); border: 1px solid var(--ec-border, #dee2e6); border-radius: 12px; overflow: hidden;"
      };
    }

    var gridMatch = declaration.match(/^ecgrid-(\d+)x(\d+)$/);
    if (gridMatch) {
      return {
        className: token,
        isMobile: isMobile,
        isTablet: isTablet,
        pseudoClass: pseudoClass,
        cssText: "display: grid; grid-template-columns: repeat(" + gridMatch[1] + ", 1fr); grid-template-rows: repeat(" + gridMatch[2] + ", 1fr);"
      };
    }

    var bounceMatch = declaration.match(/^ecbounce-(\d+)$/);
    if (bounceMatch) {
      return {
        className: token,
        isMobile: isMobile,
        isTablet: isTablet,
        pseudoClass: pseudoClass,
        cssBlock: [
          "transition: 0.2s;",
          ":hover { transform: scale(" + (1 + Number(bounceMatch[1]) / 100) + "); }",
          ":active { transform: scale(" + (1 - Number(bounceMatch[1]) / 100) + "); }"
        ]
      };
    }

    var dashIndex = declaration.search(/(?<=[a-zA-Z])-/);
    if (dashIndex === -1) {
      return null;
    }

    var rawProp = declaration.slice(0, dashIndex);
    var rawValue = declaration.slice(dashIndex + 1);

    if (!rawProp || !rawValue) {
      return null;
    }

    var cssProperty = resolveProperty(rawProp);
    var cssValue = underscoresToSpaces(rawValue);

    return {
      className: token,
      isMobile: isMobile,
      isTablet: isTablet,
      pseudoClass: pseudoClass,
      cssProperty: cssProperty,
      cssValue: cssValue,
    };
  }

  function buildCSSRule(descriptor) {
    var selector = "." + escapeClassName(descriptor.className);

    if (descriptor.pseudoClass) {
      selector = selector + ":" + descriptor.pseudoClass;
    }

    var declaration = descriptor.cssText 
      ? descriptor.cssText 
      : (descriptor.cssProperty + ": " + descriptor.cssValue + ";");
    
    if (descriptor.cssBlock) {
      var rules = [];
      descriptor.cssBlock.forEach(function (rule) {
        if (rule.startsWith(":")) {
          rules.push(selector + rule);
        } else {
          rules.push(selector + " { " + rule + " }");
        }
      });
      if (descriptor.isTablet) {
        return (
          "@media (max-width: " +
          TABLET_BREAKPOINT +
          "px) { " +
          rules.join(" ") +
          " }"
        );
      }
      if (descriptor.isMobile) {
        return (
          "@media (max-width: " +
          MOBILE_BREAKPOINT +
          "px) { " +
          rules.join(" ") +
          " }"
        );
      }
      return rules.join(" ");
    }
    if (descriptor.isTablet) {
      return (
        "@media (max-width: " +
        TABLET_BREAKPOINT +
        "px) { " +
        selector +
        " { " +
        declaration +
        " } }"
      );
    }
    if (descriptor.isMobile) {
      return (
        "@media (max-width: " +
        MOBILE_BREAKPOINT +
        "px) { " +
        selector +
        " { " +
        declaration +
        " } }"
      );
    }

    return selector + " { " + declaration + " }";
  }

  var generatedRules = {};
  var styleTag = null;

  function getStyleTag() {
    if (styleTag) {
      return styleTag;
    }
    styleTag = document.getElementById(STYLE_TAG_ID);
    if (!styleTag) {
      styleTag = document.createElement("style");
      styleTag.id = STYLE_TAG_ID;
      document.head.appendChild(styleTag);
    }
    return styleTag;
  }

  function injectRule(descriptor) {
    var key = descriptor.className;
    if (generatedRules[key]) {
      return;
    }

    var rule = buildCSSRule(descriptor);
    generatedRules[key] = rule;

    var tag = getStyleTag();
    tag.textContent += "\n" + rule;
  }

  function processElement(element) {
    if (!element.classList) {
      return;
    }
    element.classList.forEach(function (token) {
      var descriptor = parseClassName(token);
      if (descriptor) {
        injectRule(descriptor);
      }
    });
  }

  function scanDOM() {
    if (!IS_BORDER_BOX_SET) {
      var globalRule = "* { box-sizing: border-box; }";
      generatedRules["global-border-box"] = globalRule;
      getStyleTag().textContent += "\n" + globalRule;
      IS_BORDER_BOX_SET = true;
    }
    var elements = document.querySelectorAll("*:not([" + PROCESSED_ATTR + "])");
    elements.forEach(function (el) {
      processElement(el);
      el.setAttribute(PROCESSED_ATTR, "1");
    });
  }

  function startObserver() {
    var observer = new MutationObserver(function (mutations) {
      mutations.forEach(function (mutation) {
        mutation.addedNodes.forEach(function (node) {
          if (node.nodeType !== 1) {
            return;
          }
          processElement(node);
          node.querySelectorAll("*").forEach(processElement);
        });

        if (mutation.type === "attributes" && mutation.attributeName === "class") {
          processElement(mutation.target);
        }
      });
    });

    observer.observe(document.body, {
      childList: true,
      subtree: true,
      attributes: true,
      attributeFilter: ["class"],
    });
  }

  window.ECStyleSheet = {
    process: function (className) {
      var descriptor = parseClassName(className);
      if (descriptor) injectRule(descriptor);
    },
    processAll: function (classNames) {
      classNames.forEach(this.process.bind(this));
    },
    scan: scanDOM,
    getRules: function () {
      return Object.assign({}, generatedRules);
    },
    getStyleTag: getStyleTag
  };

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", function () {
      scanDOM();
      startObserver();
    });
  } else {
    scanDOM();
    startObserver();
  }
})();

(function () {
  "use strict";

  if (!window.ECStyleSheet) {
    console.error(
      "[ECElements] ECStyleSheet is required. Load ECStyleSheet.js before ECElements.js."
    );
  }

  var idCounter = 0;
  function uniqueId(prefix) {
    idCounter++;
    return (prefix || "ec") + "-" + idCounter;
  }

  var BASE_CLS = "boxSizing-border-box fontFamily--apple-system,_BlinkMacSystemFont,_'Segoe_UI',_Roboto,_sans-serif";

  var THEME_VARS_LIGHT = {
    "--ec-bg": "#ffffff",
    "--ec-surface": "#f8f9fa",
    "--ec-border": "#dee2e6",
    "--ec-text": "#212529",
    "--ec-text-muted": "#6c757d",
  };

  var THEME_VARS_DARK = {
    "--ec-bg": "#1a1a2e",
    "--ec-surface": "#16213e",
    "--ec-border": "#0f3460",
    "--ec-text": "#e0e0e0",
    "--ec-text-muted": "#a0a0b0",
  };

  function setCSSVars(el, vars) {
    for (var k in vars) {
      if (vars.hasOwnProperty(k)) el.style.setProperty(k, vars[k]);
    }
  }

  function injectKeyframes() {
    var styleId = "ec-elements-keyframes";
    if (document.getElementById(styleId)){
      return;
    }
    var tag = document.createElement("style");
    tag.id = styleId;
    tag.textContent = "@keyframes ec-spin { to { transform: rotate(360deg); } }\n" +
      "@keyframes ec-marquee { 0% { transform: translateX(0); } 100% { transform: translateX(-100%); } }";
    document.head.appendChild(tag);
  }

  var toastContainer = null;
  function getToastContainer() {
    if (toastContainer) {
      return toastContainer;
    }
    toastContainer = document.createElement("div");
    toastContainer.className = BASE_CLS + " position-fixed bottom-24px right-24px zIndex-2000 display-flex flexDirection-column gap-8px pointerEvents-none";
    document.body.appendChild(toastContainer);
    return toastContainer;
  }

  function ECTheme(options) {
    options = options || {};
    this.primary = options.primary || "#1a73e8";
    this.background = options.background || "#ffffff";
    this.text = options.text || "#212529";
    this.textMuted = options.textMuted || "#6c757d";
    this.border = options.border || "#dee2e6";
  }

  ECTheme.Blue = new ECTheme({ primary: "#1a73e8" });
  ECTheme.Green = new ECTheme({ primary: "#2e7d32" });
  ECTheme.Red = new ECTheme({ primary: "#c62828" });
  ECTheme.Purple = new ECTheme({ primary: "#6a1b9a" });
  ECTheme.Orange = new ECTheme({ primary: "#e65100" });
  ECTheme.Dark = new ECTheme({
    primary: "#4fc3f7",
    background: "#1a1a2e",
    text: "#e0e0e0",
    textMuted: "#a0a0b0",
    border: "#0f3460",
  });

  function applyBaseMixin(component) {
    var self = component;
    self.isDark = false;
    self._theme = ECTheme.Blue;

    if (self.element) {
      if (self.element.className.indexOf(BASE_CLS) === -1) {
        self.element.className = BASE_CLS + " " + self.element.className;
      }
      setCSSVars(self.element, THEME_VARS_LIGHT);
    }

    self.enableDarkMode = function () {
      self.isDark = true;
      setCSSVars(self.element, THEME_VARS_DARK);
      return self;
    };

    self.disableDarkMode = function () {
      self.isDark = false;
      setCSSVars(self.element, THEME_VARS_LIGHT);
      return self;
    };

    self.setTheme = function (theme) {
      self._theme = theme;
      self.element.style.setProperty("--ec-accent", theme.primary);
      if (theme.background !== "#ffffff" && theme.background !== "#1a1a2e") {
        self.element.style.setProperty("--ec-bg", theme.background);
        self.element.style.setProperty("--ec-text", theme.text);
        self.element.style.setProperty("--ec-text-muted", theme.textMuted);
        self.element.style.setProperty("--ec-border", theme.border);
      }
      return self;
    };

    self.setTheme(self._theme);
    return self;
  }

  function ECButton(label, options) {
    options = options || {};
    this.element = document.createElement("button");
    this.element.className = BASE_CLS + " display-inline-flex ecbounce-2 alignItems-center justifyContent-center padding-8px_18px borderRadius-8px border-none cursor-pointer fontSize-14px fontWeight-500 transition-background_0.15s_ease,_transform_0.1s_ease active:transform-scale(0.97) disabled:opacity-0.5 disabled:cursor-not-allowed";
    this.element.textContent = label || "Button";

    applyBaseMixin(this);

    if (options.variant === "outline") {
      this.element.classList.add("background-transparent", "color-var(--ec-accent,_#1a73e8)", "border-1.5px_solid_var(--ec-accent,_#1a73e8)");
    } else if (options.variant === "white") {
      this.element.classList.add("background-#ffffff", "color-var(--ec-accent,_#1a73e8)", "border-1.5px_solid_var(--ec-accent,_#1a73e8)");
    } else {
      this.element.classList.add("background-var(--ec-accent,_#1a73e8)", "color-#ffffff");
    }

    if (options.disabled) {
      this.element.disabled = true;
    }
  }

  ECButton.prototype.setLabel = function (label) { 
    this.element.textContent = label;
    return this;
  };
  
  ECButton.prototype.onClick = function (h) {
    this.element.addEventListener("click", h);
    return this;
  };
  
  ECButton.prototype.disable = function () {
    this.element.disabled = true;
    return this;
  };

  ECButton.prototype.enable = function () {
    this.element.disabled = false;
    return this;
  };

  function ECModal(title, options) {
    var self = this;
    
    options = options || {};

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-none position-fixed inset-0 background-rgba(0,0,0,0.5) zIndex-1000 alignItems-center justifyContent-center opacity-0 pointerEvents-none transition-opacity_0.22s_ease";

    this._box = document.createElement("div");
    this._box.className = "background-var(--ec-bg,_#fff) color-var(--ec-text,_#212529) border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-12px padding-24px minWidth-320px maxWidth-90vw maxHeight-85vh overflowY-auto transform-translateY(14px) transition-transform_0.22s_ease";

    if (options.width) {
      var widthValue = typeof options.width === "number" ? options.width + "px" : options.width;
      
      this._box.style.width = widthValue;
      this._box.style.maxWidth = "min(" + widthValue + ", 90vw)";
      this._box.style.minWidth = "auto";
    }

    this._header = document.createElement("div");
    this._header.className = "display-flex alignItems-center justifyContent-space-between marginBottom-16px";

    this._titleEl = document.createElement("h2");
    this._titleEl.className = "fontSize-18px fontWeight-600 margin-0 color-var(--ec-text,_#212529)";
    this._titleEl.textContent = title || "Modal";

    this._closeBtn = document.createElement("button");
    this._closeBtn.className = "background-none border-none fontSize-22px cursor-pointer color-var(--ec-text-muted,_#6c757d) padding-0_4px lineHeight-1";
    this._closeBtn.textContent = "\u00D7";
    this._closeBtn.addEventListener("click", function () {
      self.close();
    });

    this._header.appendChild(this._titleEl);
    this._header.appendChild(this._closeBtn);

    this._body = document.createElement("div");
    this._body.className = "color-var(--ec-text,_#212529)";

    this._footer = document.createElement("div");
    this._footer.className = "marginTop-20px display-flex gap-8px justifyContent-flex-end";

    this._box.appendChild(this._header);
    this._box.appendChild(this._body);
    this._box.appendChild(this._footer);
    this.element.appendChild(this._box);

    this.element.addEventListener("click", function (e) {
      if (e.target === self.element) {
        self.close();
      }
    });

    applyBaseMixin(this);
  }

  ECModal.prototype.open = function () {
    var el = this.element;
    el.classList.remove("display-none");
    el.classList.add("display-flex");
    void el.offsetHeight;
    el.classList.remove("opacity-0", "pointerEvents-none");
    el.classList.add("opacity-1", "pointerEvents-auto");
    this._box.classList.remove("transform-translateY(14px)");
    this._box.classList.add("transform-translateY(0)");
    return this;
  };

  ECModal.prototype.close = function () {
    var el = this.element;
    el.classList.remove("opacity-1", "pointerEvents-auto");
    el.classList.add("opacity-0", "pointerEvents-none");
    this._box.classList.remove("transform-translateY(0)");
    this._box.classList.add("transform-translateY(14px)");
    el.addEventListener("transitionend", function handler(e) {
      if (e.target !== el) {
        return;
      }
      el.classList.remove("display-flex");
      el.classList.add("display-none");
    },
    {
      once: true
    });
    return this;
  };

  ECModal.prototype.setTitle = function (title) {
    this._titleEl.textContent = title;
    return this;
  };

  ECModal.prototype.setContent = function (content) {
    if (typeof content === "string") {
      this._body.innerHTML = content;
    } else {
      this._body.innerHTML = "";
      this._body.appendChild(content);
    }
    return this;
  };

  ECModal.prototype.addContent = function (content) {
    if (typeof content === "string") {
      var s = document.createElement("span");
      s.innerHTML = content;
      this._body.appendChild(s);
    } else {
      this._body.appendChild(content);
    }
    return this;
  };

  ECModal.prototype.clearContent = function () {
    this._body.innerHTML = "";
    return this;
  };

  ECModal.prototype.addFooterButton = function (label, handler, variant) {
    var btn = new ECButton(label, {
      variant: variant || "filled"
    });
    btn.setTheme(this._theme);
    if (handler) {
      btn.onClick(handler);
    }
    this._footer.appendChild(btn.element);
    return this;
  };

  function ECToast(message, options) {
    options = options || {};
    this._message = message || "";
    this._duration = options.duration || 3000;
    this._type = options.type || "info";

    var accentMap = {
      info: "#1a73e8",
      success: "#2e7d32",
      warning: "#e65100",
      error: "#c62828"
    };

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " background-var(--ec-bg,_#fff) color-var(--ec-text,_#212529) border-1px_solid_var(--ec-border,_#dee2e6) borderLeftWidth-4px borderRadius-8px padding-12px_16px minWidth-240px maxWidth-360px fontSize-14px pointerEvents-auto opacity-0 transform-translateX(20px) transition-opacity_0.25s_ease,_transform_0.25s_ease boxShadow-0_4px_12px_rgba(0,0,0,0.12)";
    this.element.textContent = message;
    this.element.style.borderLeftColor = accentMap[this._type] || accentMap.info;

    applyBaseMixin(this);
  }

  ECToast.prototype.show = function () {
    var self = this;
    var container = getToastContainer();
    container.appendChild(this.element);
    requestAnimationFrame(function () {
      self.element.classList.remove("opacity-0", "transform-translateX(20px)");
      self.element.classList.add("opacity-1", "transform-translateX(0)");
    });
    setTimeout(function () {
      self.element.classList.remove("opacity-1", "transform-translateX(0)");
      self.element.classList.add("opacity-0", "transform-translateX(20px)");
      setTimeout(function () {
        if (self.element.parentNode) {
          self.element.parentNode.removeChild(self.element);
        }
      }, 300);
    }, this._duration);
    return this;
  };

  function ECRadio(name, options) {
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column gap-8px";
    this._name = name || uniqueId("radio");
    this._inputs = [];

    applyBaseMixin(this);

    if (options && Array.isArray(options)) {
      var self = this;
      options.forEach(function (opt) {
        self.addOption(opt.value, opt.label, opt.checked);
      });
    }
  }

  ECRadio.prototype.addOption = function (value, label, checked) {
    var id = uniqueId("radio-opt");
    var wrapper = document.createElement("label");
    wrapper.className = "display-flex alignItems-center gap-8px cursor-pointer fontSize-14px color-var(--ec-text,_#212529)";
    wrapper.htmlFor = id;

    var input = document.createElement("input");
    input.type = "radio";
    input.className = "width-16px height-16px cursor-pointer accentColor-var(--ec-accent,_#1a73e8)";
    input.name = this._name;
    input.value = value;
    input.id = id;
    if (checked) {
      input.checked = true;
    }

    var text = document.createElement("span");
    text.textContent = label || value;

    wrapper.appendChild(input);
    wrapper.appendChild(text);
    this.element.appendChild(wrapper);
    this._inputs.push(input);
    return this;
  };

  ECRadio.prototype.getValue = function () {
    for (var i = 0; i < this._inputs.length; i++) {
      if (this._inputs[i].checked) {
        return this._inputs[i].value;
      }
    }
    return null;
  };

  ECRadio.prototype.onChange = function (handler) {
    this.element.addEventListener("change", function (e) {
      if (e.target.type === "radio") {
        handler(e.target.value, e);
      }
    });
    return this;
  };

  function ECToggle(label, checked) {
    var self = this;
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-inline-flex alignItems-center gap-8px cursor-pointer";

    this._isChecked = checked || false;

    this._track = document.createElement("div");
    this._track.className = "width-44px height-24px borderRadius-12px position-relative transition-background_0.2s_ease flexShrink-0 background-#ccc";

    this._thumb = document.createElement("div");
    this._thumb.className = "position-absolute top-3px left-3px width-18px height-18px borderRadius-50% background-#fff transition-left_0.2s_ease boxShadow-0_1px_3px_rgba(0,0,0,0.25)";

    this._track.appendChild(this._thumb);

    this._labelEl = document.createElement("span");
    this._labelEl.className = "fontSize-14px color-var(--ec-text,_#212529) userSelect-none";
    this._labelEl.textContent = label || "";

    this.element.appendChild(this._track);
    this.element.appendChild(this._labelEl);

    this._syncState();

    this.element.addEventListener("click", function () {
      self._isChecked = !self._isChecked;
      self._syncState();
      if (self._changeHandler) {
        self._changeHandler(self._isChecked);
      }
    });

    applyBaseMixin(this);
  }

  ECToggle.prototype._syncState = function () {
    if (this._isChecked) {
      this._track.classList.replace("background-#ccc", "background-var(--ec-accent,_#1a73e8)");
      this._thumb.classList.replace("left-3px", "left-23px");
    } else {
      this._track.classList.replace("background-var(--ec-accent,_#1a73e8)", "background-#ccc");
      this._thumb.classList.replace("left-23px", "left-3px");
    }
  };

  ECToggle.prototype.getValue = function () {
    return this._isChecked;
  };
  
  ECToggle.prototype.setValue = function (val) {
    this._isChecked = !!val;
    this._syncState();
    return this;
  };
  
  ECToggle.prototype.onChange = function (h) {
    this._changeHandler = h;
    return this;
  };
  
  ECToggle.prototype.setLabel = function (lbl) {
    this._labelEl.textContent = lbl;
    return this;
  };

  function ECCheckbox(label, checked) {
    var self = this;
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-inline-flex alignItems-center gap-8px cursor-pointer";
    this._isChecked = checked || false;

    this._box = document.createElement("div");
    this._box.className = "width-16px height-16px border-2px_solid_var(--ec-accent,_#1a73e8) borderRadius-4px display-flex alignItems-center justifyContent-center background-transparent flexShrink-0 transition-background_0.15s";

    this._checkmark = document.createElement("span");
    this._checkmark.className = "color-#fff fontSize-11px fontWeight-700 lineHeight-1";
    this._checkmark.textContent = "\u2713";
    this._checkmark.style.display = "none";

    this._box.appendChild(this._checkmark);

    this._labelEl = document.createElement("span");
    this._labelEl.className = "fontSize-14px color-var(--ec-text,_#212529) userSelect-none";
    this._labelEl.textContent = label || "";

    this.element.appendChild(this._box);
    this.element.appendChild(this._labelEl);

    this._syncState();

    this.element.addEventListener("click", function () {
      self._isChecked = !self._isChecked;
      self._syncState();
      if (self._changeHandler) {
        self._changeHandler(self._isChecked);
      }
    });

    applyBaseMixin(this);
  }

  ECCheckbox.prototype._syncState = function () {
    if (this._isChecked) {
      this._box.classList.replace("background-transparent", "background-var(--ec-accent,_#1a73e8)");
      this._checkmark.style.display = "inline";
    } else {
      this._box.classList.replace("background-var(--ec-accent,_#1a73e8)", "background-transparent");
      this._checkmark.style.display = "none";
    }
  };

  ECCheckbox.prototype.getValue = function () {
    return this._isChecked;
  };
  
  ECCheckbox.prototype.setValue = function (val) {
    this._isChecked = !!val; this._syncState();
    return this;
  };
  
  ECCheckbox.prototype.onChange = function (h) {
    this._changeHandler = h;
    return this;
  };
  
  ECCheckbox.prototype.setLabel = function (lbl) {
    this._labelEl.textContent = lbl;
    return this;
  };

  function ECTextbox(options) {
    options = options || {};
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column gap-4px";

    if (options.label) {
      this._label = document.createElement("label");
      this._label.className = "fontSize-13px fontWeight-500 color-var(--ec-text-muted,_#6c757d)";
      this._label.textContent = options.label;
      this.element.appendChild(this._label);
    }

    this._input = document.createElement("input");
    this._input.type = options.type || "text";
    this._input.className = "width-100% padding-8px_12px border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-8px fontSize-14px color-var(--ec-text,_#212529) background-var(--ec-bg,_#fff) boxSizing-border-box outline-none transition-borderColor_0.15s_ease focus:borderColor-var(--ec-accent,_#1a73e8)";
    this._input.placeholder = options.placeholder || "";
    if (options.value) {
      this._input.value = options.value;
    }

    this.element.appendChild(this._input);
    applyBaseMixin(this);
  }

  ECTextbox.prototype.getValue = function () {
    return this._input.value;
  };
  
  ECTextbox.prototype.setValue = function (val) {
    this._input.value = val;
    return this;
  };

  ECTextbox.prototype.setPlaceholder = function (text) {
    this._input.placeholder = text;
    return this;
  };
  
  ECTextbox.prototype.onInput = function (h) {
    this._input.addEventListener("input", function (e) {
      h(e.target.value, e);
    });
    return this;
  };

  ECTextbox.prototype.onEnter = function (h) {
    this._input.addEventListener("keydown", function (e) {
      if (e.key === "Enter") {
        h(e.target.value, e);
      }
    });
    return this;
  };

  function ECDropdown(options) {
    options = options || {};
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column gap-4px";

    if (options.label) {
      var lbl = document.createElement("label");
      lbl.className = "fontSize-13px fontWeight-500 color-var(--ec-text-muted,_#6c757d)";
      lbl.textContent = options.label;
      this.element.appendChild(lbl);
    }

    this._select = document.createElement("select");
    this._select.className = "width-100% padding-8px_12px border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-8px fontSize-14px color-var(--ec-text,_#212529) background-var(--ec-bg,_#fff) boxSizing-border-box outline-none cursor-pointer transition-borderColor_0.15s_ease focus:borderColor-var(--ec-accent,_#1a73e8)";
    this.element.appendChild(this._select);

    if (options.items && Array.isArray(options.items)) {
      var self = this;
      options.items.forEach(function (item) {
        self.addOption(item.value, item.label);
      });
    }
    applyBaseMixin(this);
  }

  ECDropdown.prototype.addOption = function (value, label) {
    var opt = document.createElement("option");
    opt.value = value;
    opt.textContent = label || value;
    this._select.appendChild(opt);
    return this;
  };

  ECDropdown.prototype.getValue = function () {
    return this._select.value;
  };

  ECDropdown.prototype.setValue = function (val) {
    this._select.value = val;
    return this;
  };

  ECDropdown.prototype.onChange = function (h) {
    this._select.addEventListener("change", function (e) {
      h(e.target.value, e);
    });
    return this;
  };

  function ECAccordion(options) {
    options = options || {};
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-10px overflow-hidden";
    this._allowMultiple = options.allowMultiple || false;
    this._items = [];

    applyBaseMixin(this);

    if (options.items && Array.isArray(options.items)) {
      var self = this;
      options.items.forEach(function (item) {
        self.addItem(item.title, item.content, item.open);
      });
    }
  }

  ECAccordion.prototype.addItem = function (title, content, startOpen) {
    var self = this;
    var item = document.createElement("div");
    if (this._items.length > 0) {
      item.className = "borderTop-1px_solid_var(--ec-border,_#dee2e6)";
    }
    item.dataset.open = startOpen ? "true" : "false";

    var trigger = document.createElement("button");
    trigger.className = "width-100% display-flex alignItems-center justifyContent-space-between padding-14px_16px background-var(--ec-bg,_#fff) color-var(--ec-text,_#212529) fontSize-14px fontWeight-500 border-none cursor-pointer textAlign-left transition-background_0.15s hover:background-var(--ec-surface,_#f8f9fa)";

    var titleSpan = document.createElement("span");
    titleSpan.textContent = title || "Item";

    var icon = document.createElement("span");
    icon.className = "fontSize-12px transition-transform_0.22s_ease display-inline-block color-var(--ec-text-muted,_#6c757d)";
    icon.textContent = "▼";
    if (startOpen) {
      icon.classList.add("transform-rotate(180deg)");
    }

    trigger.appendChild(titleSpan);
    trigger.appendChild(icon);

    var body = document.createElement("div");
    var baseBodyClass = "overflow-hidden transition-maxHeight_0.25s_ease,_padding_0.25s_ease fontSize-14px color-var(--ec-text,_#212529) boxSizing-border-box ";
    body.className = baseBodyClass + (startOpen ? "maxHeight-600px padding-12px_16px" : "maxHeight-0 padding-0_16px");

    if (typeof content === "string") {
      body.innerHTML = content;
    } else if (content instanceof HTMLElement) {
      body.appendChild(content);
    }

    trigger.addEventListener("click", function () {
      var isOpen = item.dataset.open === "true";
      if (!self._allowMultiple) {
        self._items.forEach(function (i) {
          if (i !== item && i.dataset.open === "true") {
            i.dataset.open = "false";
            i._icon.classList.remove("transform-rotate(180deg)");
            i._body.classList.replace("maxHeight-600px", "maxHeight-0");
            i._body.classList.replace("padding-12px_16px", "padding-0_16px");
          }
        });
      }
      if (!isOpen) {
        item.dataset.open = "true";
        icon.classList.add("transform-rotate(180deg)");
        body.classList.replace("maxHeight-0", "maxHeight-600px");
        body.classList.replace("padding-0_16px", "padding-12px_16px");
      } else {
        item.dataset.open = "false";
        icon.classList.remove("transform-rotate(180deg)");
        body.classList.replace("maxHeight-600px", "maxHeight-0");
        body.classList.replace("padding-12px_16px", "padding-0_16px");
      }
    });

    item._icon = icon;
    item._body = body;
    item.appendChild(trigger);
    item.appendChild(body);
    this.element.appendChild(item);
    this._items.push(item);
    return this;
  };

  function ECStepper(steps, currentStep) {
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex alignItems-flex-start gap-0 width-100%";
    this._steps = steps || [];
    this._current = currentStep !== undefined ? currentStep : 0;

    applyBaseMixin(this);
    this._render();
  }

  ECStepper.prototype._render = function () {
    var self = this;
    this.element.innerHTML = "";

    this._steps.forEach(function (label, i) {
      var step = document.createElement("div");
      step.className = "display-flex flexDirection-column alignItems-center flex-1 position-relative";

      if (i < self._steps.length - 1) {
        step.className += " after:content-'' after:position-absolute after:top-14px after:left-calc(50%_+_14px) after:right-calc(-50%_+_14px) after:height-2px after:transition-background_0.3s after:zIndex-0";
        if (i < self._current) {
          step.className += " after:background-var(--ec-accent,_#1a73e8)";
        } else {
          step.className += " after:background-var(--ec-border,_#dee2e6)";
        }
      }

      var circle = document.createElement("div");
      circle.className = "width-28px height-28px borderRadius-50% border-2px_solid_var(--ec-border,_#dee2e6) display-flex alignItems-center justifyContent-center fontSize-12px fontWeight-600 zIndex-1 transition-background_0.2s,_borderColor_0.2s,_color_0.2s position-relative";
      circle.textContent = i < self._current ? "✓" : String(i + 1);

      if (i < self._current) {
        circle.classList.add("background-var(--ec-accent,_#1a73e8)", "borderColor-var(--ec-accent,_#1a73e8)", "color-#fff");
      } else if (i === self._current) {
        circle.classList.add("background-var(--ec-bg,_#fff)", "borderColor-var(--ec-accent,_#1a73e8)", "color-var(--ec-accent,_#1a73e8)");
      } else {
        circle.classList.add("background-var(--ec-bg,_#fff)", "color-var(--ec-text-muted,_#6c757d)");
      }

      var lbl = document.createElement("div");
      lbl.className = "marginTop-6px fontSize-12px fontWeight-500 textAlign-center";
      
      if (i < self._current) {
        lbl.classList.add("color-var(--ec-text,_#212529)");
      } else if (i === self._current) {
        lbl.classList.add("color-var(--ec-accent,_#1a73e8)");
      } else {
        lbl.classList.add("color-var(--ec-text-muted,_#6c757d)");
      }

      lbl.textContent = label;
      step.appendChild(circle);
      step.appendChild(lbl);
      self.element.appendChild(step);
    });
  };

  ECStepper.prototype.setStep = function (index) {
    this._current = Math.max(0, Math.min(index, this._steps.length - 1));
    this._render();
    return this;
  };

  ECStepper.prototype.next = function () {
    return this.setStep(this._current + 1);
  };

  ECStepper.prototype.prev = function () {
    return this.setStep(this._current - 1);
  };

  ECStepper.prototype.getStep = function () {
    return this._current;
  };

  function ECDivider(options) {
    options = options || {};
    this._label = options.label || null;

    if (this._label) {
      this.element = document.createElement("div");
      this.element.className = BASE_CLS + " display-flex alignItems-center gap-12px margin-16px_0";
      
      var left = document.createElement("hr");
      left.className = "borderLeft-none borderRight-none borderBottom-none borderTop-1px_solid_var(--ec-border,_#dee2e6) margin-0 flex-1";

      var span = document.createElement("span");
      span.className = "fontSize-12px fontWeight-500 color-var(--ec-text-muted,_#6c757d) whiteSpace-nowrap";
      span.textContent = this._label;

      var right = document.createElement("hr");
      right.className = "borderLeft-none borderRight-none borderBottom-none borderTop-1px_solid_var(--ec-border,_#dee2e6) margin-0 flex-1";

      this.element.appendChild(left);
      this.element.appendChild(span);
      this.element.appendChild(right);
    } else {
      this.element = document.createElement("hr");
      this.element.className = BASE_CLS + " borderLeft-none borderRight-none borderBottom-none";
      this.element.classList.add("borderTop-1px_solid_var(--ec-border,_#dee2e6)", "margin-16px_0", "width-100%");
      if (options.thick) {
        this.element.classList.add("borderTopWidth-2px");
      }
      if (options.dashed) {
        this.element.classList.add("borderTopStyle-dashed");
      }
    }

    applyBaseMixin(this);
  }

  function ECProgressBar(options) {
    options = options || {};
    this._value = Math.min(100, Math.max(0, options.value || 0));
    this._label = options.label || null;
    this._showPercent = options.showPercent !== false;

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column gap-6px width-100%";

    if (this._label || this._showPercent) {
      this._header = document.createElement("div");
      this._header.className = "display-flex alignItems-center justifyContent-space-between";

      if (this._label) {
        this._labelEl = document.createElement("span");
        this._labelEl.className = "fontSize-13px fontWeight-500 color-var(--ec-text,_#212529)";
        this._labelEl.textContent = this._label;
        this._header.appendChild(this._labelEl);
      }

      if (this._showPercent) {
        this._pctEl = document.createElement("span");
        this._pctEl.className = "fontSize-12px color-var(--ec-text-muted,_#6c757d)";
        this._pctEl.textContent = this._value + "%";
        this._header.appendChild(this._pctEl);
      }
      this.element.appendChild(this._header);
    }

    this._track = document.createElement("div");
    this._track.className = "width-100% background-var(--ec-surface,_#f8f9fa) borderRadius-999px overflow-hidden height-8px border-1px_solid_var(--ec-border,_#dee2e6)";

    this._bar = document.createElement("div");
    this._bar.className = "height-100% borderRadius-999px background-var(--ec-accent,_#1a73e8) transition-width_0.4s_ease";
    this._bar.style.width = this._value + "%";

    this._track.appendChild(this._bar);
    this.element.appendChild(this._track);

    applyBaseMixin(this);
  }

  ECProgressBar.prototype.setValue = function (val) {
    this._value = Math.min(100, Math.max(0, val));
    this._bar.style.width = this._value + "%";
    if (this._pctEl) {
      this._pctEl.textContent = this._value + "%";
    }
    return this;
  };

  ECProgressBar.prototype.getValue = function () {
    return this._value;
  };

  ECProgressBar.prototype.setLabel = function (label) {
    if (this._labelEl) {
      this._labelEl.textContent = label;
    }
    return this;
  };

  ECProgressBar.prototype.setHeight = function (px) {
    this._track.style.height = typeof px === "number" ? px + "px" : px;
    return this;
  };

  function ECSpinner(options) {
    options = options || {};
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-inline-block border-3px_solid_var(--ec-border,_#dee2e6) borderTopColor-var(--ec-accent,_#1a73e8) borderRadius-50% animation-ec-spin_0.7s_linear_infinite";
    this.setSize(options.size || "md");
    applyBaseMixin(this);
  }

  ECSpinner.prototype.setSize = function (size) {
    this.element.classList.remove("width-16px", "height-16px", "borderWidth-2px", "width-40px", "height-40px", "borderWidth-4px", "width-24px", "height-24px");
    if (size === "sm") {
      this.element.classList.add("width-16px", "height-16px", "borderWidth-2px");
    } else if (size === "lg") {
      this.element.classList.add("width-40px", "height-40px", "borderWidth-4px");
    } else {
      this.element.classList.add("width-24px", "height-24px");
    }
    return this;
  };

  function ECTooltip(targetEl, text, options) {
    var self = this;
    options = options || {};
    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " position-relative display-inline-flex";

    if (targetEl && targetEl.element) {
      targetEl = targetEl.element;
    }
    if (targetEl instanceof HTMLElement) {
      this.element.appendChild(targetEl);
    }

    this._box = document.createElement("div");
    this._box.className = "position-absolute bottom-calc(100%_+_6px) left-50% transform-translateX(-50%) background-#1e1e2e color-#e0e0e0 fontSize-12px padding-5px_10px borderRadius-6px whiteSpace-nowrap pointerEvents-none opacity-0 transition-opacity_0.15s_ease zIndex-3000 after:content-'' after:position-absolute after:top-100% after:left-50% after:transform-translateX(-50%) after:border-5px_solid_transparent after:borderTopColor-#1e1e2e";
    this._box.textContent = text || "";
    this.element.appendChild(this._box);

    this.element.addEventListener("mouseenter", function () {
      self._box.classList.replace("opacity-0", "opacity-1");
    });
    this.element.addEventListener("mouseleave", function () {
      self._box.classList.replace("opacity-1", "opacity-0");
    });

    applyBaseMixin(this);
  }

  ECTooltip.prototype.setText = function (text) {
    this._box.textContent = text;
    return this;
  };

  function ECPopup(triggerEl, options) {
    options = options || {};
    var self = this;

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " position-relative display-inline-flex";

    if (triggerEl && triggerEl.element) {
      triggerEl = triggerEl.element;
    }
    if (triggerEl instanceof HTMLElement) {
      this.element.appendChild(triggerEl);
      this._trigger = triggerEl;
    }

    this._box = document.createElement("div");
    this._box.className = "display-none position-absolute top-calc(100%_+_8px) left-0 minWidth-180px background-var(--ec-bg,_#fff) color-var(--ec-text,_#212529) border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-10px padding-12px zIndex-2500 boxShadow-0_4px_16px_rgba(0,0,0,0.10) opacity-0 transform-translateY(6px) transition-opacity_0.18s_ease,_transform_0.18s_ease";
    this.element.appendChild(this._box);

    if (this._trigger) {
      this._trigger.addEventListener("click", function (e) {
        e.stopPropagation();
        self._isOpen ? self.close() : self.open();
      });
    }

    this._isOpen = false;
    this._outsideHandler = null;
    applyBaseMixin(this);
  }

  ECPopup.prototype.open = function () {
    var self = this, box = this._box;
    box.classList.replace("display-none", "display-block");
    void box.offsetHeight;
    box.classList.remove("opacity-0", "transform-translateY(6px)");
    box.classList.add("opacity-1", "transform-translateY(0)");
    this._isOpen = true;
    setTimeout(function () {
      self._outsideHandler = function (e) {
        if (!self.element.contains(e.target)) {
          self.close();
        }
      };
      document.addEventListener("click", self._outsideHandler);
    }, 0);
    return this;
  };

  ECPopup.prototype.close = function () {
    if (!this._isOpen) {
      return this;
    }
    var box = this._box;
    box.classList.remove("opacity-1", "transform-translateY(0)");
    box.classList.add("opacity-0", "transform-translateY(6px)");
    box.addEventListener("transitionend", function () {
      box.classList.replace("display-block", "display-none");
    },
    {
      once: true
    });
    this._isOpen = false;
    if (this._outsideHandler) {
      document.removeEventListener("click", this._outsideHandler);
      this._outsideHandler = null;
    }
    return this;
  };

  ECPopup.prototype.setContent = function (content) {
    if (typeof content === "string") {
      this._box.innerHTML = content;
    } else if (content instanceof HTMLElement) {
      this._box.innerHTML = "";
      this._box.appendChild(content);
    }
    return this;
  };
  ECPopup.prototype.addContent = function (content) {
    if (typeof content === "string") {
      var span = document.createElement("span");
      span.innerHTML = content;
      this._box.appendChild(span);
    } else if (content instanceof HTMLElement) {
      this._box.appendChild(content);
    }
    return this;
  };
  
  ECPopup.prototype.setWidth = function (width) {
    this._box.style.minWidth = typeof width === "number" ? width + "px" : width;
    return this;
  };

  function ECDataTable(options) {
    options = options || {};
    this._columns  = options.columns  || [];
    this._data     = options.data     || [];
    this._pageSize = options.pageSize || 10;
    this._page     = 0;
    this._sortKey  = null;
    this._sortDir  = 1;
    this._filter   = "";

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " width-100% overflowX-auto border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-10px";
    applyBaseMixin(this);
    this._build();
  }

  ECDataTable.prototype._filtered = function () {
    var q = this._filter.toLowerCase();
    if (!q) {
      return this._data.slice();
    }
    return this._data.filter(function (row) {
      return Object.values(row).some(function (v) {
        return String(v).toLowerCase().indexOf(q) !== -1;
      });
    });
  };

  ECDataTable.prototype._sorted = function (rows) {
    if (!this._sortKey) {
      return rows;
    }
    var key = this._sortKey, dir = this._sortDir;
    return rows.slice().sort(function (a, b) {
      var av = a[key], bv = b[key];
      if (av == null) {
        return 1;
      }
      if (bv == null) {
        return -1;
      }
      if (!isNaN(av) && !isNaN(bv)) {
        return (Number(av) - Number(bv)) * dir;
      }
      return String(av).localeCompare(String(bv)) * dir;
    });
  };

  ECDataTable.prototype._build = function () {
    var self = this;
    this.element.innerHTML = "";

    var toolbar = document.createElement("div");
    toolbar.className = "display-flex alignItems-center justifyContent-space-between padding-10px_14px borderBottom-1px_solid_var(--ec-border,_#dee2e6) gap-10px flexWrap-wrap";

    var search = document.createElement("input");
    search.className = "padding-6px_10px border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-6px fontSize-13px color-var(--ec-text,_#212529) background-var(--ec-bg,_#fff) outline-none minWidth-180px focus:borderColor-var(--ec-accent,_#1a73e8)";
    search.type = "text";
    search.placeholder = "Search…";
    search.value = this._filter;
    search.addEventListener("input", function () {
      self._filter = search.value;
      self._page = 0;
      self._build();
    });

    this._infoEl = document.createElement("span");
    this._infoEl.className = "fontSize-12px color-var(--ec-text-muted,_#6c757d)";

    toolbar.appendChild(search);
    toolbar.appendChild(this._infoEl);
    this.element.appendChild(toolbar);

    var table = document.createElement("table");
    table.className = "width-100% borderCollapse-collapse fontSize-14px";

    var thead = document.createElement("thead");
    var headRow = document.createElement("tr");
    this._columns.forEach(function (col) {
      var th = document.createElement("th");
      th.className = "padding-10px_14px textAlign-left fontSize-12px fontWeight-600 color-var(--ec-text-muted,_#6c757d) borderBottom-1px_solid_var(--ec-border,_#dee2e6) background-var(--ec-surface,_#f8f9fa) cursor-pointer whiteSpace-nowrap userSelect-none hover:color-var(--ec-text,_#212529)";
      th.textContent = col.label || col.key;
      var icon = document.createElement("span");
      icon.className = "marginLeft-4px opacity-0.4 fontSize-10px";
      icon.textContent = "▲▼";
      if (col.sortable !== false) {
        th.appendChild(icon);
        th.addEventListener("click", function () {
          if (self._sortKey === col.key) {
            self._sortDir *= -1;
          } else {
            self._sortKey = col.key;
            self._sortDir = 1;
          }
          self._page = 0;
          self._build();
        });
        if (self._sortKey === col.key) {
          icon.classList.remove("opacity-0.4");
          icon.classList.add("opacity-1", "color-var(--ec-accent,_#1a73e8)");
          icon.textContent = self._sortDir === 1 ? "▲" : "▼";
        }
      }
      headRow.appendChild(th);
    });
    thead.appendChild(headRow);
    table.appendChild(thead);

    var filtered = this._sorted(this._filtered());
    var total    = filtered.length;
    var start    = this._page * this._pageSize;
    var pageRows = filtered.slice(start, start + this._pageSize);

    this._infoEl.textContent = "Showing " + (total === 0 ? 0 : start + 1) + "–" + Math.min(start + this._pageSize, total) + " of " + total;

    var tbody = document.createElement("tbody");
    pageRows.forEach(function (row, rIdx) {
      var tr = document.createElement("tr");
      tr.addEventListener("mouseenter", function () { tr.classList.add("background-var(--ec-surface,_#f8f9fa)"); });
      tr.addEventListener("mouseleave", function () { tr.classList.remove("background-var(--ec-surface,_#f8f9fa)"); });

      self._columns.forEach(function (col) {
        var td = document.createElement("td");
        td.className = "padding-10px_14px color-var(--ec-text,_#212529) verticalAlign-middle";
        if (rIdx < pageRows.length - 1) {
          td.classList.add("borderBottom-1px_solid_var(--ec-border,_#dee2e6)");
        }

        var val = row[col.key];
        if (col.render) {
          var rendered = col.render(val, row);
          if (rendered instanceof HTMLElement) {
            td.appendChild(rendered);
          } else {
            td.innerHTML = rendered;
          }
        } else {
          td.textContent = val != null ? val : "";
        }
        tr.appendChild(td);
      });
      tbody.appendChild(tr);
    });
    table.appendChild(tbody);
    this.element.appendChild(table);

    var footer = document.createElement("div");
    footer.className = "display-flex alignItems-center justifyContent-space-between padding-8px_14px borderTop-1px_solid_var(--ec-border,_#dee2e6) flexWrap-wrap gap-8px";

    var totalPages = Math.max(1, Math.ceil(total / this._pageSize));
    var pageInfo = document.createElement("span");
    pageInfo.className = "fontSize-12px color-var(--ec-text-muted,_#6c757d)";
    pageInfo.textContent = "Page " + (this._page + 1) + " of " + totalPages;

    var pageNav = document.createElement("div");
    pageNav.className = "display-flex gap-4px";

    var navBtnCls = "padding-4px_10px border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-6px background-var(--ec-bg,_#fff) color-var(--ec-text,_#212529) fontSize-13px cursor-pointer transition-background_0.15s hover:background-var(--ec-surface,_#f8f9fa) disabled:opacity-0.4 disabled:cursor-not-allowed";

    var prevBtn = document.createElement("button");
    prevBtn.className = navBtnCls;
    prevBtn.textContent = "← Prev";
    prevBtn.disabled = this._page === 0;
    prevBtn.addEventListener("click", function () { self._page--; self._build(); });

    var nextBtn = document.createElement("button");
    nextBtn.className = navBtnCls;
    nextBtn.textContent = "Next →";
    nextBtn.disabled = this._page >= totalPages - 1;
    nextBtn.addEventListener("click", function () { self._page++; self._build(); });

    pageNav.appendChild(prevBtn);
    pageNav.appendChild(nextBtn);
    footer.appendChild(pageInfo);
    footer.appendChild(pageNav);
    this.element.appendChild(footer);
  };

  ECDataTable.prototype.setData = function (data) {
    this._data = data || [];
    this._page = 0;
    this._build();
    return this;
  };

  ECDataTable.prototype.setColumns = function (cols) {
    this._columns = cols || [];
    this._build();
    return this;
  };

  ECDataTable.prototype.setPageSize = function (n) {
    this._pageSize = n;
    this._page = 0;
    this._build();
    return this;
  };

  function ECSlider(options) {
    options = options || {};
    this._min    = options.min   !== undefined ? options.min   : 0;
    this._max    = options.max   !== undefined ? options.max   : 100;
    this._step   = options.step  !== undefined ? options.step  : 1;
    this._value  = options.value !== undefined ? options.value : this._min;
    this._label  = options.label || null;
    this._ticks  = options.ticks || null;
    this._suffix = options.suffix || "";

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " display-flex flexDirection-column gap-6px width-100%";

    if (this._label) {
      var header = document.createElement("div");
      header.className = "display-flex justifyContent-space-between alignItems-center";

      var lbl = document.createElement("span");
      lbl.className = "fontSize-13px fontWeight-500 color-var(--ec-text,_#212529)";
      lbl.textContent = this._label;

      this._valueEl = document.createElement("span");
      this._valueEl.className = "fontSize-13px fontWeight-600 color-var(--ec-accent,_#1a73e8) minWidth-32px textAlign-right";
      this._valueEl.textContent = this._value + this._suffix;

      header.appendChild(lbl);
      header.appendChild(this._valueEl);
      this.element.appendChild(header);
    }

    this._input = document.createElement("input");
    this._input.type  = "range";
    this._input.className = "width-100% height-4px borderRadius-999px outline-none cursor-pointer accentColor-var(--ec-accent,_#1a73e8)";
    this._input.min   = this._min;
    this._input.max   = this._max;
    this._input.step  = this._step;
    this._input.value = this._value;

    var self = this;
    this._input.addEventListener("input", function () {
      self._value = Number(self._input.value);
      if (self._valueEl) {
        self._valueEl.textContent = self._value + self._suffix;
      }
      if (self._changeHandler) {
        self._changeHandler(self._value);
      }
    });

    this.element.appendChild(this._input);

    if (this._ticks && this._ticks.length) {
      var tickRow = document.createElement("div");
      tickRow.className = "display-flex justifyContent-space-between padding-0_2px";
      this._ticks.forEach(function (t) {
        var span = document.createElement("span");
        span.className = "fontSize-11px color-var(--ec-text-muted,_#6c757d)";
        span.textContent = t;
        tickRow.appendChild(span);
      });
      this.element.appendChild(tickRow);
    }

    applyBaseMixin(this);
  }

  ECSlider.prototype.getValue = function () {
    return this._value;
  };

  ECSlider.prototype.setValue = function (val) {
    this._value = Math.min(this._max, Math.max(this._min, val));
    this._input.value = this._value;
    if (this._valueEl) {
      this._valueEl.textContent = this._value + this._suffix;
    }
    return this;
  };

  ECSlider.prototype.onChange = function (h) {
    this._changeHandler = h;
    return this;
  };

  var MONTHS = ["January","February","March","April","May","June","July","August","September","October","November","December"];
  var DAYS   = ["Su","Mo","Tu","We","Th","Fr","Sa"];

  function ECDatePicker(options) {
    options = options || {};
    var self = this;
    this._value     = options.value || null;
    this._label     = options.label || null;
    this._isOpen    = false;
    this._outsideHandler = null;

    var now = new Date();
    this._viewYear  = now.getFullYear();
    this._viewMonth = now.getMonth();

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " position-relative display-inline-block width-100%";

    if (this._label) {
      var lbl = document.createElement("label");
      lbl.className = "fontSize-13px fontWeight-500 color-var(--ec-text-muted,_#6c757d)";
      lbl.textContent = this._label;
      this.element.appendChild(lbl);
    }

    this._trigger = document.createElement("button");
    this._trigger.type = "button";
    this._trigger.className = "width-100% padding-8px_12px border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-8px fontSize-14px color-var(--ec-text,_#212529) background-var(--ec-bg,_#fff) boxSizing-border-box cursor-pointer outline-none textAlign-left display-flex alignItems-center justifyContent-space-between hover:borderColor-var(--ec-accent,_#1a73e8) focus:borderColor-var(--ec-accent,_#1a73e8)";

    this._triggerText = document.createElement("span");
    this._triggerText.textContent = this._value || "Select a date";
    this._triggerText.style.color = this._value ? "inherit" : "var(--ec-text-muted,#6c757d)";

    var calIcon = document.createElement("span");
    calIcon.textContent = "📅";
    calIcon.className = "fontSize-14px";

    this._trigger.appendChild(this._triggerText);
    this._trigger.appendChild(calIcon);
    this.element.appendChild(this._trigger);

    this._cal = document.createElement("div");
    this._cal.className = "display-none position-absolute top-calc(100%_+_6px) left-0 background-var(--ec-bg,_#fff) border-1px_solid_var(--ec-border,_#dee2e6) borderRadius-10px padding-12px zIndex-3000 width-280px boxShadow-0_4px_16px_rgba(0,0,0,0.10) opacity-0 transform-translateY(6px) transition-opacity_0.18s_ease,_transform_0.18s_ease";
    this.element.appendChild(this._cal);

    this._trigger.addEventListener("click", function (e) {
      e.stopPropagation();
      self._isOpen ? self._closeCal() : self._openCal();
    });

    applyBaseMixin(this);
    this._renderCal();
  }

  ECDatePicker.prototype._openCal = function () {
    var cal = this._cal, self = this;
    cal.classList.replace("display-none", "display-block");
    void cal.offsetHeight;
    cal.classList.remove("opacity-0", "transform-translateY(6px)");
    cal.classList.add("opacity-1", "transform-translateY(0)");
    this._isOpen = true;
    setTimeout(function () {
      self._outsideHandler = function (e) {
        if (!self.element.contains(e.target)) {
          self._closeCal();
        }
      };
      document.addEventListener("click", self._outsideHandler);
    }, 0);
  };

  ECDatePicker.prototype._closeCal = function () {
    var cal = this._cal;
    cal.classList.remove("opacity-1", "transform-translateY(0)");
    cal.classList.add("opacity-0", "transform-translateY(6px)");
    cal.addEventListener("transitionend", function () {
      cal.classList.replace("display-block", "display-none");
    },
    {
      once: true
    });
    this._isOpen = false;
    if (this._outsideHandler) {
      document.removeEventListener("click", this._outsideHandler);
      this._outsideHandler = null;
    }
  };

  ECDatePicker.prototype._renderCal = function () {
    var self = this;
    this._cal.innerHTML = "";

    var header = document.createElement("div");
    header.className = "display-flex alignItems-center justifyContent-space-between marginBottom-10px";

    var navCls = "background-none border-none fontSize-16px cursor-pointer color-var(--ec-text,_#212529) padding-2px_6px borderRadius-4px transition-background_0.15s hover:background-var(--ec-surface,_#f8f9fa)";
    var prev = document.createElement("button");
    prev.className = navCls; prev.type = "button"; prev.textContent = "‹";
    prev.addEventListener("click", function (e) {
      e.stopPropagation();
      self._viewMonth--;
      if (self._viewMonth < 0) {
        self._viewMonth = 11;
        self._viewYear--;
      }
      self._renderCal();
    });

    var next = document.createElement("button");
    next.className = navCls; next.type = "button";
    next.textContent = "›";
    next.addEventListener("click", function (e) {
      e.stopPropagation(); self._viewMonth++;
      if (self._viewMonth > 11) {
        self._viewMonth = 0;
        self._viewYear++;
      }
      self._renderCal();
    });

    var monthLabel = document.createElement("span");
    monthLabel.className = "fontSize-14px fontWeight-600 color-var(--ec-text,_#212529)";
    monthLabel.textContent = MONTHS[self._viewMonth] + " " + self._viewYear;

    header.appendChild(prev);
    header.appendChild(monthLabel);
    header.appendChild(next);
    this._cal.appendChild(header);

    var grid = document.createElement("div");
    grid.className = "display-grid gridTemplateColumns-repeat(7,_1fr) gap-2px";

    DAYS.forEach(function (d) {
      var dow = document.createElement("div");
      dow.className = "fontSize-11px fontWeight-600 color-var(--ec-text-muted,_#6c757d) textAlign-center padding-4px_0";
      dow.textContent = d;
      grid.appendChild(dow);
    });

    var today = new Date();
    var firstDay = new Date(self._viewYear, self._viewMonth, 1).getDay();
    var daysInMonth = new Date(self._viewYear, self._viewMonth + 1, 0).getDate();

    for (var i = 0; i < firstDay; i++) {
      var empty = document.createElement("button");
      empty.className = "cursor-default border-none background-none width-100% padding-6px_2px";
      empty.disabled = true;
      grid.appendChild(empty);
    }

    for (var d = 1; d <= daysInMonth; d++) {
      (function (day) {
        var btn = document.createElement("button");
        btn.type = "button";
        btn.className = "fontSize-13px textAlign-center padding-6px_2px borderRadius-6px cursor-pointer color-var(--ec-text,_#212529) transition-background_0.12s border-none background-none width-100% hover:background-var(--ec-surface,_#f8f9fa)";
        btn.textContent = day;

        var iso = self._viewYear + "-" + String(self._viewMonth + 1).padStart(2, "0") + "-" + String(day).padStart(2, "0");

        if (today.getFullYear() === self._viewYear && today.getMonth() === self._viewMonth && today.getDate() === day) {
          btn.classList.add("fontWeight-700", "color-var(--ec-accent,_#1a73e8)");
        }

        if (self._value === iso) {
          btn.classList.add("background-var(--ec-accent,_#1a73e8)", "color-#fff");
        }

        btn.addEventListener("click", function (e) {
          e.stopPropagation();
          self._value = iso;
          self._triggerText.textContent = iso;
          self._triggerText.style.color = "inherit";
          self._renderCal();
          self._closeCal();
          if (self._changeHandler) {
            self._changeHandler(iso);
          }
        });

        grid.appendChild(btn);
      })(d);
    }
    this._cal.appendChild(grid);
  };

  ECDatePicker.prototype.getValue = function () {
    return this._value;
  };

  ECDatePicker.prototype.setValue = function (iso) {
    this._value = iso;
    this._triggerText.textContent = iso || "Select a date";
    this._triggerText.style.color = iso ? "inherit" : "var(--ec-text-muted,#6c757d)";
    this._renderCal();
    return this;
  };

  ECDatePicker.prototype.onChange = function (h) {
    this._changeHandler = h;
    return this;
  };

  function ECFileUpload(options) {
    options = options || {};
    var self = this;
    this._accept   = options.accept   || "*";
    this._multiple = options.multiple !== false;
    this._maxSize  = options.maxSize  || null;
    this._files    = [];

    this.element = document.createElement("div");
    this.element.className = BASE_CLS + " border-2px_dashed_var(--ec-border,_#dee2e6) borderRadius-10px padding-32px_20px textAlign-center cursor-pointer background-var(--ec-bg,_#fff) transition-borderColor_0.2s,_background_0.2s boxSizing-border-box hover:borderColor-var(--ec-accent,_#1a73e8) hover:background-var(--ec-surface,_#f8f9fa)";

    var icon = document.createElement("div");
    icon.className = "fontSize-32px marginBottom-8px";
    icon.textContent = "☁️";

    var title = document.createElement("p");
    title.className = "fontSize-15px fontWeight-600 color-var(--ec-text,_#212529) margin-0_0_4px";
    title.textContent = options.title || "Drag & Drop files here";

    var sub = document.createElement("p");
    sub.className = "fontSize-13px color-var(--ec-text-muted,_#6c757d) margin-0_0_12px";
    sub.textContent = options.subtitle || "or click to browse";

    var browseBtn = document.createElement("span");
    browseBtn.className = "display-inline-block padding-6px_16px background-var(--ec-accent,_#1a73e8) color-#fff borderRadius-6px fontSize-13px fontWeight-500 cursor-pointer";
    browseBtn.textContent = "Browse Files";

    this._fileList = document.createElement("div");
    this._fileList.className = "marginTop-12px textAlign-left display-flex flexDirection-column gap-6px";

    this._input = document.createElement("input");
    this._input.type = "file";
    this._input.accept = this._accept;
    this._input.multiple = this._multiple;
    this._input.className = "display-none";

    this.element.appendChild(icon);
    this.element.appendChild(title);
    this.element.appendChild(sub);
    this.element.appendChild(browseBtn);
    this.element.appendChild(this._fileList);
    this.element.appendChild(this._input);

    this.element.addEventListener("click", function () { self._input.click(); });
    this._input.addEventListener("change", function () { self._addFiles(Array.from(self._input.files)); self._input.value = ""; });

    this.element.addEventListener("dragover", function (e) {
      e.preventDefault();
      self.element.classList.add("borderColor-var(--ec-accent,_#1a73e8)", "background-var(--ec-surface,_#f8f9fa)");
    });
    this.element.addEventListener("dragleave", function () {
      self.element.classList.remove("borderColor-var(--ec-accent,_#1a73e8)", "background-var(--ec-surface,_#f8f9fa)");
    });
    this.element.addEventListener("drop", function (e) {
      e.preventDefault();
      self.element.classList.remove("borderColor-var(--ec-accent,_#1a73e8)", "background-var(--ec-surface,_#f8f9fa)");
      self._addFiles(Array.from(e.dataTransfer.files));
    });

    applyBaseMixin(this);
  }

  ECFileUpload.prototype._addFiles = function (files) {
    var self = this;
    files.forEach(function (file) {
      if (self._maxSize && file.size > self._maxSize) {
        new ECToast("\"" + file.name + "\" exceeds the size limit.", { type: "warning", duration: 3000 }).show();
        return;
      }
      self._files.push(file);
      self._renderFile(file);
      if (self._changeHandler) {
        self._changeHandler(self._files);
      }
    });
  };

  ECFileUpload.prototype._renderFile = function (file) {
    var self = this;
    var row = document.createElement("div");
    row.className = "display-flex alignItems-center justifyContent-space-between padding-6px_10px background-var(--ec-surface,_#f8f9fa) borderRadius-6px fontSize-13px color-var(--ec-text,_#212529) border-1px_solid_var(--ec-border,_#dee2e6)";

    var name = document.createElement("span");
    var kb = (file.size / 1024).toFixed(1);
    name.textContent = file.name + " (" + kb + " KB)";

    var removeBtn = document.createElement("a");
    removeBtn.className = "background-none border-none display-flex alignItems-center justifyContent-center color-var(--ec-text-muted,_#6c757d) cursor-pointer fontSize-16px lineHeight-1 height-16px width-16px";
    removeBtn.innerHTML = `<svg class="ecfileupload_close_icon" xmlns="http://www.w3.org/2000/svg" height="12px" viewBox="0 -960 960 960" width="12px" fill="#6c757d"><path d="M480-424 284-228q-11 11-28 11t-28-11q-11-11-11-28t11-28l196-196-196-196q-11-11-11-28t11-28q11-11 28-11t28 11l196 196 196-196q11-11 28-11t28 11q11 11 11 28t-11 28L536-480l196 196q11 11 11 28t-11 28q-11 11-28 11t-28-11L480-424Z"/></svg>`;
    removeBtn.addEventListener("mouseenter", function () {
      document.querySelector(".ecfileupload_close_icon").setAttribute("fill", "#c62828");
    });
    removeBtn.addEventListener("mouseleave", function () {
      document.querySelector(".ecfileupload_close_icon").setAttribute("fill", "#6c757d");
    });
    removeBtn.addEventListener("click", function (e) {
      e.stopPropagation();
      self._files = self._files.filter(function (f) { return f !== file; });
      row.parentNode.removeChild(row);
      if (self._changeHandler) {
        self._changeHandler(self._files);
      }
    });

    row.appendChild(name);
    row.appendChild(removeBtn);
    this._fileList.appendChild(row);
  };

  ECFileUpload.prototype.getFiles = function () {
    return this._files.slice();
  };

  ECFileUpload.prototype.clear = function () {
    this._files = [];
    this._fileList.innerHTML = "";
    return this;
  };

  ECFileUpload.prototype.onChange = function (h) {
    this._changeHandler = h;
    return this;
  };

  function ECBasicCard(content) {
    this.element = document.createElement("div");
    
    this.element.className = BASE_CLS + " eccard padding-20px boxSizing-border-box";

    if (content) {
      this.append(content);
    }

    applyBaseMixin(this);
  }

  ECBasicCard.prototype.append = function (child) {
    if (typeof child === "string") {
      var span = document.createElement("span");
      span.innerHTML = child;
      this.element.appendChild(span);
    } else if (child && child.element) {
      this.element.appendChild(child.element);
    } else if (child instanceof HTMLElement) {
      this.element.appendChild(child);
    }
    return this;
  };

  ECBasicCard.prototype.setContent = function (content) {
    this.element.innerHTML = "";
    this.append(content);
    return this;
  };

  function boot() {
    injectKeyframes();
  }

  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", boot);
  } else {
    boot();
  }

  window.ECTheme = ECTheme;
  window.ECButton = ECButton;
  window.ECModal = ECModal;
  window.ECToast = ECToast;
  window.ECRadio = ECRadio;
  window.ECToggle = ECToggle;
  window.ECCheckbox = ECCheckbox;
  window.ECTextbox = ECTextbox;
  window.ECDropdown = ECDropdown;
  window.ECAccordion = ECAccordion;
  window.ECStepper = ECStepper;
  window.ECDivider = ECDivider;
  window.ECProgressBar = ECProgressBar;
  window.ECSpinner = ECSpinner;
  window.ECTooltip = ECTooltip;
  window.ECPopup = ECPopup;
  window.ECDataTable = ECDataTable;
  window.ECSlider = ECSlider;
  window.ECDatePicker = ECDatePicker;
  window.ECFileUpload = ECFileUpload;
  window.ECBasicCard = ECBasicCard;
})();