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
        private string pseudo;
        private UserStat globalStat;
        private List<Skin> skins;

        private Skin selectedBall;
        private Skin selectedPaddle;

        public BallSkin SelectedBall { get { return (BallSkin)selectedBall; } }
        public PaddleSkin SelectedPaddle { get { return (PaddleSkin)selectedPaddle; } }

        public User(string pseudo)
        {
            this.pseudo = pseudo;
            globalStat = new UserStat();
            skins = new List<Skin>();

            selectedPaddle = new PaddleSkin("Form/paddle", "simplePaddle");
            selectedBall = new BallSkin("Form/ball", "simpleBall");

            skins.Add(selectedBall);
            skins.Add(selectedPaddle);
        }

        public void AddSkin(Skin skin) { skins.Add(skin); }
        public void RemoveSkin(Skin skin) {  skins.Remove(skin); }
    }
}
