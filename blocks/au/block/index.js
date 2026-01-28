const K = process.env.NODE_ENV !== "production" ? Object.freeze({}) : {}, ke = process.env.NODE_ENV !== "production" ? Object.freeze([]) : [], se = () => {
}, Re = (e) => e.charCodeAt(0) === 111 && e.charCodeAt(1) === 110 && // uppercase letter
(e.charCodeAt(2) > 122 || e.charCodeAt(2) < 97), x = Object.assign, d = Array.isArray, De = (e) => Q(e) === "[object Map]", Te = (e) => Q(e) === "[object Set]", S = (e) => typeof e == "function", N = (e) => typeof e == "string", G = (e) => typeof e == "symbol", E = (e) => e !== null && typeof e == "object", ie = Object.prototype.toString, Q = (e) => ie.call(e), xe = (e) => Q(e) === "[object Object]";
let oe;
const H = () => oe || (oe = typeof globalThis < "u" ? globalThis : typeof self < "u" ? self : typeof window < "u" ? window : typeof global < "u" ? global : {});
function X(e) {
  if (d(e)) {
    const t = {};
    for (let n = 0; n < e.length; n++) {
      const o = e[n], s = N(o) ? Pe(o) : X(o);
      if (s)
        for (const r in s)
          t[r] = s[r];
    }
    return t;
  } else if (N(e) || E(e))
    return e;
}
const Fe = /;(?![^(]*\))/g, Ie = /:([^]+)/, $e = /\/\*[^]*?\*\//g;
function Pe(e) {
  const t = {};
  return e.replace($e, "").split(Fe).forEach((n) => {
    if (n) {
      const o = n.split(Ie);
      o.length > 1 && (t[o[0].trim()] = o[1].trim());
    }
  }), t;
}
function Z(e) {
  let t = "";
  if (N(e))
    t = e;
  else if (d(e))
    for (let n = 0; n < e.length; n++) {
      const o = Z(e[n]);
      o && (t += o + " ");
    }
  else if (E(e))
    for (const n in e)
      e[n] && (t += n + " ");
  return t.trim();
}
const ce = (e) => !!(e && e.__v_isRef === !0), le = (e) => N(e) ? e : e == null ? "" : d(e) || E(e) && (e.toString === ie || !S(e.toString)) ? ce(e) ? le(e.value) : JSON.stringify(e, ue, 2) : String(e), ue = (e, t) => ce(t) ? ue(e, t.value) : De(t) ? {
  [`Map(${t.size})`]: [...t.entries()].reduce(
    (n, [o, s], r) => (n[U(o, r) + " =>"] = s, n),
    {}
  )
} : Te(t) ? {
  [`Set(${t.size})`]: [...t.values()].map((n) => U(n))
} : G(t) ? U(t) : E(t) && !d(t) && !xe(t) ? String(t) : t, U = (e, t = "") => {
  var n;
  return (
    // Symbol.description in es2019+ so we need to cast here to pass
    // the lib: es2016 check
    G(e) ? `Symbol(${(n = e.description) != null ? n : t})` : e
  );
};
process.env.NODE_ENV;
process.env.NODE_ENV;
process.env.NODE_ENV;
new Set(
  /* @__PURE__ */ Object.getOwnPropertyNames(Symbol).filter((e) => e !== "arguments" && e !== "caller").map((e) => Symbol[e]).filter(G)
);
// @__NO_SIDE_EFFECTS__
function ae(e) {
  return /* @__PURE__ */ q(e) ? /* @__PURE__ */ ae(e.__v_raw) : !!(e && e.__v_isReactive);
}
// @__NO_SIDE_EFFECTS__
function q(e) {
  return !!(e && e.__v_isReadonly);
}
// @__NO_SIDE_EFFECTS__
function z(e) {
  return !!(e && e.__v_isShallow);
}
// @__NO_SIDE_EFFECTS__
function W(e) {
  return e ? !!e.__v_raw : !1;
}
// @__NO_SIDE_EFFECTS__
function O(e) {
  const t = e && e.__v_raw;
  return t ? /* @__PURE__ */ O(t) : e;
}
// @__NO_SIDE_EFFECTS__
function v(e) {
  return e ? e.__v_isRef === !0 : !1;
}
const w = [];
function Ae(e) {
  w.push(e);
}
function Me() {
  w.pop();
}
let B = !1;
function R(e, ...t) {
  if (B) return;
  B = !0;
  const n = w.length ? w[w.length - 1].component : null, o = n && n.appContext.config.warnHandler, s = je();
  if (o)
    ee(
      o,
      n,
      11,
      [
        // eslint-disable-next-line no-restricted-syntax
        e + t.map((r) => {
          var c, l;
          return (l = (c = r.toString) == null ? void 0 : c.call(r)) != null ? l : JSON.stringify(r);
        }).join(""),
        n && n.proxy,
        s.map(
          ({ vnode: r }) => `at <${Ve(n, r.type)}>`
        ).join(`
`),
        s
      ]
    );
  else {
    const r = [`[Vue warn]: ${e}`, ...t];
    s.length && r.push(`
`, ...He(s)), console.warn(...r);
  }
  B = !1;
}
function je() {
  let e = w[w.length - 1];
  if (!e)
    return [];
  const t = [];
  for (; e; ) {
    const n = t[0];
    n && n.vnode === e ? n.recurseCount++ : t.push({
      vnode: e,
      recurseCount: 0
    });
    const o = e.component && e.component.parent;
    e = o && o.vnode;
  }
  return t;
}
function He(e) {
  const t = [];
  return e.forEach((n, o) => {
    t.push(...o === 0 ? [] : [`
`], ...Ue(n));
  }), t;
}
function Ue({ vnode: e, recurseCount: t }) {
  const n = t > 0 ? `... (${t} recursive calls)` : "", o = e.component ? e.component.parent == null : !1, s = ` at <${Ve(
    e.component,
    e.type,
    o
  )}`, r = ">" + n;
  return e.props ? [s, ...ze(e.props), r] : [s + r];
}
function ze(e) {
  const t = [], n = Object.keys(e);
  return n.slice(0, 3).forEach((o) => {
    t.push(...fe(o, e[o]));
  }), n.length > 3 && t.push(" ..."), t;
}
function fe(e, t, n) {
  return N(t) ? (t = JSON.stringify(t), n ? t : [`${e}=${t}`]) : typeof t == "number" || typeof t == "boolean" || t == null ? n ? t : [`${e}=${t}`] : /* @__PURE__ */ v(t) ? (t = fe(e, /* @__PURE__ */ O(t.value), !0), n ? t : [`${e}=Ref<`, t, ">"]) : S(t) ? [`${e}=fn${t.name ? `<${t.name}>` : ""}`] : (t = /* @__PURE__ */ O(t), n ? t : [`${e}=`, t]);
}
const pe = {
  sp: "serverPrefetch hook",
  bc: "beforeCreate hook",
  c: "created hook",
  bm: "beforeMount hook",
  m: "mounted hook",
  bu: "beforeUpdate hook",
  u: "updated",
  bum: "beforeUnmount hook",
  um: "unmounted hook",
  a: "activated hook",
  da: "deactivated hook",
  ec: "errorCaptured hook",
  rtc: "renderTracked hook",
  rtg: "renderTriggered hook",
  0: "setup function",
  1: "render function",
  2: "watcher getter",
  3: "watcher callback",
  4: "watcher cleanup function",
  5: "native event handler",
  6: "component event handler",
  7: "vnode hook",
  8: "directive hook",
  9: "transition hook",
  10: "app errorHandler",
  11: "app warnHandler",
  12: "ref function",
  13: "async component loader",
  14: "scheduler flush",
  15: "component update",
  16: "app unmount cleanup function"
};
function ee(e, t, n, o) {
  try {
    return o ? e(...o) : e();
  } catch (s) {
    de(s, t, n);
  }
}
function de(e, t, n, o = !0) {
  const s = t ? t.vnode : null, { errorHandler: r, throwUnhandledErrorInProduction: c } = t && t.appContext.config || K;
  if (t) {
    let l = t.parent;
    const u = t.proxy, m = process.env.NODE_ENV !== "production" ? pe[n] : `https://vuejs.org/error-reference/#runtime-${n}`;
    for (; l; ) {
      const g = l.ec;
      if (g) {
        for (let i = 0; i < g.length; i++)
          if (g[i](e, u, m) === !1)
            return;
      }
      l = l.parent;
    }
    if (r) {
      ee(r, null, 10, [
        e,
        u,
        m
      ]);
      return;
    }
  }
  Be(e, n, s, o, c);
}
function Be(e, t, n, o = !0, s = !1) {
  if (process.env.NODE_ENV !== "production") {
    const r = pe[t];
    if (n && Ae(n), R(`Unhandled error${r ? ` during execution of ${r}` : ""}`), n && Me(), o)
      throw e;
    console.error(e);
  } else {
    if (s)
      throw e;
    console.error(e);
  }
}
const p = [];
let y = -1;
const k = [];
let b = null, V = 0;
const Je = /* @__PURE__ */ Promise.resolve();
let Y = null;
const Le = 100;
function Ke(e) {
  let t = y + 1, n = p.length;
  for (; t < n; ) {
    const o = t + n >>> 1, s = p[o], r = T(s);
    r < e || r === e && s.flags & 2 ? t = o + 1 : n = o;
  }
  return t;
}
function qe(e) {
  if (!(e.flags & 1)) {
    const t = T(e), n = p[p.length - 1];
    !n || // fast path when the job id is larger than the tail
    !(e.flags & 2) && t >= T(n) ? p.push(e) : p.splice(Ke(t), 0, e), e.flags |= 1, _e();
  }
}
function _e() {
  Y || (Y = Je.then(me));
}
function We(e) {
  d(e) ? k.push(...e) : b && e.id === -1 ? b.splice(V + 1, 0, e) : e.flags & 1 || (k.push(e), e.flags |= 1), _e();
}
function Ye(e) {
  if (k.length) {
    const t = [...new Set(k)].sort(
      (n, o) => T(n) - T(o)
    );
    if (k.length = 0, b) {
      b.push(...t);
      return;
    }
    for (b = t, process.env.NODE_ENV !== "production" && (e = e || /* @__PURE__ */ new Map()), V = 0; V < b.length; V++) {
      const n = b[V];
      process.env.NODE_ENV !== "production" && he(e, n) || (n.flags & 4 && (n.flags &= -2), n.flags & 8 || n(), n.flags &= -2);
    }
    b = null, V = 0;
  }
}
const T = (e) => e.id == null ? e.flags & 2 ? -1 : 1 / 0 : e.id;
function me(e) {
  process.env.NODE_ENV !== "production" && (e = e || /* @__PURE__ */ new Map());
  const t = process.env.NODE_ENV !== "production" ? (n) => he(e, n) : se;
  try {
    for (y = 0; y < p.length; y++) {
      const n = p[y];
      if (n && !(n.flags & 8)) {
        if (process.env.NODE_ENV !== "production" && t(n))
          continue;
        n.flags & 4 && (n.flags &= -2), ee(
          n,
          n.i,
          n.i ? 15 : 14
        ), n.flags & 4 || (n.flags &= -2);
      }
    }
  } finally {
    for (; y < p.length; y++) {
      const n = p[y];
      n && (n.flags &= -2);
    }
    y = -1, p.length = 0, Ye(e), Y = null, (p.length || k.length) && me(e);
  }
}
function he(e, t) {
  const n = e.get(t) || 0;
  if (n > Le) {
    const o = t.i, s = o && we(o.type);
    return de(
      `Maximum recursive updates exceeded${s ? ` in component <${s}>` : ""}. This means you have a reactive effect that is mutating its own dependencies and thus recursively triggering itself. Possible sources include component template, render function, updated hook or watcher source function.`,
      null,
      10
    ), !0;
  }
  return e.set(t, n + 1), !1;
}
const J = /* @__PURE__ */ new Map();
process.env.NODE_ENV !== "production" && (H().__VUE_HMR_RUNTIME__ = {
  createRecord: L(Ge),
  rerender: L(Qe),
  reload: L(Xe)
});
const P = /* @__PURE__ */ new Map();
function Ge(e, t) {
  return P.has(e) ? !1 : (P.set(e, {
    initialDef: A(t),
    instances: /* @__PURE__ */ new Set()
  }), !0);
}
function A(e) {
  return Ce(e) ? e.__vccOpts : e;
}
function Qe(e, t) {
  const n = P.get(e);
  n && (n.initialDef.render = t, [...n.instances].forEach((o) => {
    t && (o.render = t, A(o.type).render = t), o.renderCache = [], o.job.flags & 8 || o.update();
  }));
}
function Xe(e, t) {
  const n = P.get(e);
  if (!n) return;
  t = A(t), re(n.initialDef, t);
  const o = [...n.instances];
  for (let s = 0; s < o.length; s++) {
    const r = o[s], c = A(r.type);
    let l = J.get(c);
    l || (c !== n.initialDef && re(c, t), J.set(c, l = /* @__PURE__ */ new Set())), l.add(r), r.appContext.propsCache.delete(r.type), r.appContext.emitsCache.delete(r.type), r.appContext.optionsCache.delete(r.type), r.ceReload ? (l.add(r), r.ceReload(t.styles), l.delete(r)) : r.parent ? qe(() => {
      r.job.flags & 8 || (r.parent.update(), l.delete(r));
    }) : r.appContext.reload ? r.appContext.reload() : typeof window < "u" ? window.location.reload() : console.warn(
      "[HMR] Root or manually mounted instance modified. Full reload required."
    ), r.root.ce && r !== r.root && r.root.ce._removeChildStyle(c);
  }
  We(() => {
    J.clear();
  });
}
function re(e, t) {
  x(e, t);
  for (const n in e)
    n !== "__file" && !(n in t) && delete e[n];
}
function L(e) {
  return (t, n) => {
    try {
      return e(t, n);
    } catch (o) {
      console.error(o), console.warn(
        "[HMR] Something went wrong during Vue component hot-reload. Full reload required."
      );
    }
  };
}
let C, F = [];
function ge(e, t) {
  var n, o;
  C = e, C ? (C.enabled = !0, F.forEach(({ event: s, args: r }) => C.emit(s, ...r)), F = []) : /* handle late devtools injection - only do this if we are in an actual */ /* browser environment to avoid the timer handle stalling test runner exit */ /* (#4815) */ typeof window < "u" && // some envs mock window but not fully
  window.HTMLElement && // also exclude jsdom
  // eslint-disable-next-line no-restricted-syntax
  !((o = (n = window.navigator) == null ? void 0 : n.userAgent) != null && o.includes("jsdom")) ? ((t.__VUE_DEVTOOLS_HOOK_REPLAY__ = t.__VUE_DEVTOOLS_HOOK_REPLAY__ || []).push((r) => {
    ge(r, t);
  }), setTimeout(() => {
    C || (t.__VUE_DEVTOOLS_HOOK_REPLAY__ = null, F = []);
  }, 3e3)) : F = [];
}
let M = null, Ze = null;
const ve = (e) => e.__isTeleport;
function ye(e, t) {
  e.shapeFlag & 6 && e.component ? (e.transition = t, ye(e.component.subTree, t)) : e.shapeFlag & 128 ? (e.ssContent.transition = t.clone(e.ssContent), e.ssFallback.transition = t.clone(e.ssFallback)) : e.transition = t;
}
// @__NO_SIDE_EFFECTS__
function et(e, t) {
  return S(e) ? (
    // #8236: extend call and options.name access are considered side-effects
    // by Rollup, so we have to wrap it in a pure-annotated IIFE.
    x({ name: e.name }, t, { setup: e })
  ) : e;
}
H().requestIdleCallback;
H().cancelIdleCallback;
const tt = /* @__PURE__ */ Symbol.for("v-ndc"), nt = {};
process.env.NODE_ENV !== "production" && (nt.ownKeys = (e) => (R(
  "Avoid app logic that relies on enumerating keys on a component instance. The keys will be empty in production mode to avoid performance overhead."
), Reflect.ownKeys(e)));
const ot = {}, Ee = (e) => Object.getPrototypeOf(e) === ot, rt = (e) => e.__isSuspense, Ne = /* @__PURE__ */ Symbol.for("v-fgt"), st = /* @__PURE__ */ Symbol.for("v-txt"), it = /* @__PURE__ */ Symbol.for("v-cmt"), I = [];
let _ = null;
function ct(e = !1) {
  I.push(_ = e ? null : []);
}
function lt() {
  I.pop(), _ = I[I.length - 1] || null;
}
function ut(e) {
  return e.dynamicChildren = _ || ke, lt(), _ && _.push(e), e;
}
function at(e, t, n, o, s, r) {
  return ut(
    te(
      e,
      t,
      n,
      o,
      s,
      r,
      !0
    )
  );
}
function ft(e) {
  return e ? e.__v_isVNode === !0 : !1;
}
const pt = (...e) => be(
  ...e
), Se = ({ key: e }) => e ?? null, $ = ({
  ref: e,
  ref_key: t,
  ref_for: n
}) => (typeof e == "number" && (e = "" + e), e != null ? N(e) || /* @__PURE__ */ v(e) || S(e) ? { i: M, r: e, k: t, f: !!n } : e : null);
function te(e, t = null, n = null, o = 0, s = null, r = e === Ne ? 0 : 1, c = !1, l = !1) {
  const u = {
    __v_isVNode: !0,
    __v_skip: !0,
    type: e,
    props: t,
    key: t && Se(t),
    ref: t && $(t),
    scopeId: Ze,
    slotScopeIds: null,
    children: n,
    component: null,
    suspense: null,
    ssContent: null,
    ssFallback: null,
    dirs: null,
    transition: null,
    el: null,
    anchor: null,
    target: null,
    targetStart: null,
    targetAnchor: null,
    staticCount: 0,
    shapeFlag: r,
    patchFlag: o,
    dynamicProps: s,
    dynamicChildren: null,
    appContext: null,
    ctx: M
  };
  return l ? (ne(u, n), r & 128 && e.normalize(u)) : n && (u.shapeFlag |= N(n) ? 8 : 16), process.env.NODE_ENV !== "production" && u.key !== u.key && R("VNode created with invalid key (NaN). VNode type:", u.type), // avoid a block node from tracking itself
  !c && // has current parent block
  _ && // presence of a patch flag indicates this node needs patching on updates.
  // component nodes also should always be patched, because even if the
  // component doesn't need to update, it needs to persist the instance on to
  // the next vnode so that it can be properly unmounted later.
  (u.patchFlag > 0 || r & 6) && // the EVENTS flag is only for hydration and if it is the only flag, the
  // vnode should not be considered dynamic due to handler caching.
  u.patchFlag !== 32 && _.push(u), u;
}
const dt = process.env.NODE_ENV !== "production" ? pt : be;
function be(e, t = null, n = null, o = 0, s = null, r = !1) {
  if ((!e || e === tt) && (process.env.NODE_ENV !== "production" && !e && R(`Invalid vnode type when creating vnode: ${e}.`), e = it), ft(e)) {
    const l = j(
      e,
      t,
      !0
      /* mergeRef: true */
    );
    return n && ne(l, n), !r && _ && (l.shapeFlag & 6 ? _[_.indexOf(e)] = l : _.push(l)), l.patchFlag = -2, l;
  }
  if (Ce(e) && (e = e.__vccOpts), t) {
    t = _t(t);
    let { class: l, style: u } = t;
    l && !N(l) && (t.class = Z(l)), E(u) && (/* @__PURE__ */ W(u) && !d(u) && (u = x({}, u)), t.style = X(u));
  }
  const c = N(e) ? 1 : rt(e) ? 128 : ve(e) ? 64 : E(e) ? 4 : S(e) ? 2 : 0;
  return process.env.NODE_ENV !== "production" && c & 4 && /* @__PURE__ */ W(e) && (e = /* @__PURE__ */ O(e), R(
    "Vue received a Component that was made a reactive object. This can lead to unnecessary performance overhead and should be avoided by marking the component with `markRaw` or using `shallowRef` instead of `ref`.",
    `
Component that was made reactive: `,
    e
  )), te(
    e,
    t,
    n,
    o,
    s,
    c,
    r,
    !0
  );
}
function _t(e) {
  return e ? /* @__PURE__ */ W(e) || Ee(e) ? x({}, e) : e : null;
}
function j(e, t, n = !1, o = !1) {
  const { props: s, ref: r, patchFlag: c, children: l, transition: u } = e, m = t ? ht(s || {}, t) : s, g = {
    __v_isVNode: !0,
    __v_skip: !0,
    type: e.type,
    props: m,
    key: m && Se(m),
    ref: t && t.ref ? (
      // #2078 in the case of <component :is="vnode" ref="extra"/>
      // if the vnode itself already has a ref, cloneVNode will need to merge
      // the refs so the single vnode can be set on multiple refs
      n && r ? d(r) ? r.concat($(t)) : [r, $(t)] : $(t)
    ) : r,
    scopeId: e.scopeId,
    slotScopeIds: e.slotScopeIds,
    children: process.env.NODE_ENV !== "production" && c === -1 && d(l) ? l.map(Oe) : l,
    target: e.target,
    targetStart: e.targetStart,
    targetAnchor: e.targetAnchor,
    staticCount: e.staticCount,
    shapeFlag: e.shapeFlag,
    // if the vnode is cloned with extra props, we can no longer assume its
    // existing patch flag to be reliable and need to add the FULL_PROPS flag.
    // note: preserve flag for fragments since they use the flag for children
    // fast paths only.
    patchFlag: t && e.type !== Ne ? c === -1 ? 16 : c | 16 : c,
    dynamicProps: e.dynamicProps,
    dynamicChildren: e.dynamicChildren,
    appContext: e.appContext,
    dirs: e.dirs,
    transition: u,
    // These should technically only be non-null on mounted VNodes. However,
    // they *should* be copied for kept-alive vnodes. So we just always copy
    // them since them being non-null during a mount doesn't affect the logic as
    // they will simply be overwritten.
    component: e.component,
    suspense: e.suspense,
    ssContent: e.ssContent && j(e.ssContent),
    ssFallback: e.ssFallback && j(e.ssFallback),
    placeholder: e.placeholder,
    el: e.el,
    anchor: e.anchor,
    ctx: e.ctx,
    ce: e.ce
  };
  return u && o && ye(
    g,
    u.clone(g)
  ), g;
}
function Oe(e) {
  const t = j(e);
  return d(e.children) && (t.children = e.children.map(Oe)), t;
}
function mt(e = " ", t = 0) {
  return dt(st, null, e, t);
}
function ne(e, t) {
  let n = 0;
  const { shapeFlag: o } = e;
  if (t == null)
    t = null;
  else if (d(t))
    n = 16;
  else if (typeof t == "object")
    if (o & 65) {
      const s = t.default;
      s && (s._c && (s._d = !1), ne(e, s()), s._c && (s._d = !0));
      return;
    } else
      n = 32, !t._ && !Ee(t) && (t._ctx = M);
  else S(t) ? (t = { default: t, _ctx: M }, n = 32) : (t = String(t), o & 64 ? (n = 16, t = [mt(t)]) : n = 8);
  e.children = t, e.shapeFlag |= n;
}
function ht(...e) {
  const t = {};
  for (let n = 0; n < e.length; n++) {
    const o = e[n];
    for (const s in o)
      if (s === "class")
        t.class !== o.class && (t.class = Z([t.class, o.class]));
      else if (s === "style")
        t.style = X([t.style, o.style]);
      else if (Re(s)) {
        const r = t[s], c = o[s];
        c && r !== c && !(d(r) && r.includes(c)) && (t[s] = r ? [].concat(r, c) : c);
      } else s !== "" && (t[s] = o[s]);
  }
  return t;
}
{
  const e = H(), t = (n, o) => {
    let s;
    return (s = e[n]) || (s = e[n] = []), s.push(o), (r) => {
      s.length > 1 ? s.forEach((c) => c(r)) : s[0](r);
    };
  };
  t(
    "__VUE_INSTANCE_SETTERS__",
    (n) => n
  ), t(
    "__VUE_SSR_SETTERS__",
    (n) => n
  );
}
process.env.NODE_ENV;
const gt = /(?:^|[-_])\w/g, yt = (e) => e.replace(gt, (t) => t.toUpperCase()).replace(/[-_]/g, "");
function we(e, t = !0) {
  return S(e) ? e.displayName || e.name : e.name || t && e.__name;
}
function Ve(e, t, n = !1) {
  let o = we(t);
  if (!o && t.__file) {
    const s = t.__file.match(/([^/\\]+)\.\w+$/);
    s && (o = s[1]);
  }
  if (!o && e) {
    const s = (r) => {
      for (const c in r)
        if (r[c] === t)
          return c;
    };
    o = s(e.components) || e.parent && s(
      e.parent.type.components
    ) || s(e.appContext.components);
  }
  return o ? yt(o) : n ? "App" : "Anonymous";
}
function Ce(e) {
  return S(e) && "__vccOpts" in e;
}
function Et() {
  if (process.env.NODE_ENV === "production" || typeof window > "u")
    return;
  const e = { style: "color:#3ba776" }, t = { style: "color:#1677ff" }, n = { style: "color:#f5222d" }, o = { style: "color:#eb2f96" }, s = {
    __vue_custom_formatter: !0,
    header(i) {
      if (!E(i))
        return null;
      if (i.__isVue)
        return ["div", e, "VueInstance"];
      if (/* @__PURE__ */ v(i)) {
        const a = i.value;
        return [
          "div",
          {},
          ["span", e, g(i)],
          "<",
          l(a),
          ">"
        ];
      } else {
        if (/* @__PURE__ */ ae(i))
          return [
            "div",
            {},
            ["span", e, /* @__PURE__ */ z(i) ? "ShallowReactive" : "Reactive"],
            "<",
            l(i),
            `>${/* @__PURE__ */ q(i) ? " (readonly)" : ""}`
          ];
        if (/* @__PURE__ */ q(i))
          return [
            "div",
            {},
            ["span", e, /* @__PURE__ */ z(i) ? "ShallowReadonly" : "Readonly"],
            "<",
            l(i),
            ">"
          ];
      }
      return null;
    },
    hasBody(i) {
      return i && i.__isVue;
    },
    body(i) {
      if (i && i.__isVue)
        return [
          "div",
          {},
          ...r(i.$)
        ];
    }
  };
  function r(i) {
    const a = [];
    i.type.props && i.props && a.push(c("props", /* @__PURE__ */ O(i.props))), i.setupState !== K && a.push(c("setup", i.setupState)), i.data !== K && a.push(c("data", /* @__PURE__ */ O(i.data)));
    const f = u(i, "computed");
    f && a.push(c("computed", f));
    const h = u(i, "inject");
    return h && a.push(c("injected", h)), a.push([
      "div",
      {},
      [
        "span",
        {
          style: o.style + ";opacity:0.66"
        },
        "$ (internal): "
      ],
      ["object", { object: i }]
    ]), a;
  }
  function c(i, a) {
    return a = x({}, a), Object.keys(a).length ? [
      "div",
      { style: "line-height:1.25em;margin-bottom:0.6em" },
      [
        "div",
        {
          style: "color:#476582"
        },
        i
      ],
      [
        "div",
        {
          style: "padding-left:1.25em"
        },
        ...Object.keys(a).map((f) => [
          "div",
          {},
          ["span", o, f + ": "],
          l(a[f], !1)
        ])
      ]
    ] : ["span", {}];
  }
  function l(i, a = !0) {
    return typeof i == "number" ? ["span", t, i] : typeof i == "string" ? ["span", n, JSON.stringify(i)] : typeof i == "boolean" ? ["span", o, i] : E(i) ? ["object", { object: a ? /* @__PURE__ */ O(i) : i }] : ["span", n, String(i)];
  }
  function u(i, a) {
    const f = i.type;
    if (S(f))
      return;
    const h = {};
    for (const D in i.ctx)
      m(f, D, a) && (h[D] = i.ctx[D]);
    return h;
  }
  function m(i, a, f) {
    const h = i[f];
    if (d(h) && h.includes(a) || E(h) && a in h || i.extends && m(i.extends, a, f) || i.mixins && i.mixins.some((D) => m(D, a, f)))
      return !0;
  }
  function g(i) {
    return /* @__PURE__ */ z(i) ? "ShallowRef" : i.effect ? "ComputedRef" : "Ref";
  }
  window.devtoolsFormatters ? window.devtoolsFormatters.push(s) : window.devtoolsFormatters = [s];
}
process.env.NODE_ENV;
process.env.NODE_ENV;
process.env.NODE_ENV;
function Nt() {
  Et();
}
process.env.NODE_ENV !== "production" && Nt();
const St = { class: "remote-block" }, bt = /* @__PURE__ */ et({
  __name: "Block",
  props: {
    title: {}
  },
  setup(e) {
    return (t, n) => (ct(), at("div", St, [
      te("h3", null, le(e.title), 1)
    ]));
  }
}), Ot = (e, t) => {
  const n = e.__vccOpts || e;
  for (const [o, s] of t)
    n[o] = s;
  return n;
}, wt = /* @__PURE__ */ Ot(bt, [["__scopeId", "data-v-c025b4a4"]]);
export {
  wt as default
};
