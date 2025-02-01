using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [REF]
//  qiita: Unityで学ぶデザインパターン08: Composite パターン https://qiita.com/Cova8bitdot/items/1c57d856027a33e99eb0
//  qiita: Compositeパターン https://qiita.com/WestRiver/items/11c48ec3929322e296a7

namespace Nitou.DesignPattern.Demo {

    public class Character {
        public void Heal(float amount) { }
        public void ClearDebuffAll() { }
    }

    public interface ISkill {
        void Invoke(Character[] targets);
    }


    public class CompositeSkill : ISkill, ICollection<ISkill> {

        protected readonly object _gate = new ();
        protected List<ISkill> list = new ();

        // ===== ISkill =====

        void ISkill.Invoke(Character[] targets) {
            if (targets == null) return;
            foreach (var skill in list) {
                skill.Invoke(targets);
            }
        }

        // ===== ICollection =====

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(ISkill item) { list.Add(item); }

        public void Clear() { list.Clear(); }

        public bool Contains(ISkill item) {
            return list.Contains(item);
        }

        public void CopyTo(ISkill[] array, int arrayIndex) {
            int startIndex = Mathf.Min(arrayIndex, array.Length - 1);
            for (int i = startIndex; i < array.Length; i++) {
                Add(array[i]);
            }
        }

        public bool Remove(ISkill item) {
            return list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<ISkill> GetEnumerator() {
            var res = new List<ISkill>();

            lock (_gate) {
                foreach (var d in list) {
                    if (d != null) res.Add(d);
                }
            }

            return res.GetEnumerator();
        }
    }


    /// <summary>
    /// HP完全回復スキル
    /// </summary>
    public class FullHealSkill : ISkill {
        void ISkill.Invoke(Character[] targets) {
            if (targets == null) return;
            foreach (var target in targets) {
                target.Heal(100.0f);
            }
        }
    }

    /// <summary>
    /// 状態異常回復スキル
    /// </summary>
    public class FullCure : ISkill {
        void ISkill.Invoke(Character[] targets) {
            if (targets == null) return;
            foreach (var target in targets) {
                target.ClearDebuffAll();
            }
        }
    }


    /// <summary>
    /// 完全回復スキル
    /// </summary>
    public class PerfectHeal : CompositeSkill {
        public PerfectHeal() {
            // 完全回復 = HP全回復 + 全状態異常回復
            list.Add(new FullHealSkill());
            list.Add(new FullCure());
        }
    }
}
