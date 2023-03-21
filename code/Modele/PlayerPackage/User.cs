using Modele.EntityPackage;
using Modele.SkinPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modele.PlayerPackage
{
    public class User
    {
        public string Pseudo { get; private set; }
        public UserStat GlobalStat { get; private set; }
        private List<Skin> skins;

        private Skin selectedPaddle;
        public PaddleSkin SelectedPaddle { get { return (PaddleSkin)selectedPaddle; } set { this.selectedPaddle = value; } }

        public User(string pseudo)
        {
            Pseudo = pseudo;
            GlobalStat = new UserStat();
            skins = new List<Skin>();

            selectedPaddle = new PaddleSkin("Form/paddle", "simplePaddle");

            skins.Add(selectedPaddle);
        }

        public void AddSkin(Skin skin) { skins.Add(skin); }
        public void RemoveSkin(Skin skin) {  skins.Remove(skin); }
    }
}
