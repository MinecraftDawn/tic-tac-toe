namespace tic_tac_toe.utils {
    public class ReverseDictionary<TKey, TValue> {
        private Dictionary<TKey, TValue> keyToValue = new Dictionary<TKey, TValue>();
        private Dictionary<TValue, HashSet<TKey>> valueToKeys = new Dictionary<TValue, HashSet<TKey>>();

        public void Set(TKey key, TValue value) {
            if (keyToValue.ContainsKey(key)) {
                this.Remove(key);
            }

            keyToValue[key] = value;
            if (valueToKeys.ContainsKey(value)) {
                valueToKeys[value].Add(key);
            } else {
                valueToKeys[value] = new HashSet<TKey> { key };
            }
        }

        public bool Remove(TKey key) {
            if (keyToValue.TryGetValue(key, out TValue value)) {
                keyToValue.Remove(key);
                valueToKeys[value].Remove(key);
                if (valueToKeys[value].Count == 0) {
                    valueToKeys.Remove(value);
                }
                return true;
            }
            return false;
        }

        public TValue GetValue(TKey key) {
            return keyToValue[key];
        }

        public bool ContainsKey(TKey key) {
            return keyToValue.TryGetValue(key, out TValue value);
        }

        public HashSet<TKey> GetKeysForValue(TValue value) {
            if (valueToKeys.ContainsKey(value)) {
                return valueToKeys[value];
            } else {
                return new HashSet<TKey>();
            }
        }

        public int CountKeysForValue(TValue value) {
            return GetKeysForValue(value).Count;
        }
    }
}
