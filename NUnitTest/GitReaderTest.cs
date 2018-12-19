using Android.Content;
using GiHub_MVVM.Core.Common;
using GiHub_MVVM.Core.Models;
using GiHub_MVVM.Core.Services;
using Moq;
using MvvmCross.Test.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class GitReaderTest : MvxIoCSupportingTest
    {
        protected override void AdditionalSetup()
        {
            var listServiceMoc = new Mock<IGitReposService>();
            var repos = new List<GitRepository>() { new GitRepository() };
            var task = Task.Run(() => repos);
            Ioc.RegisterSingleton(listServiceMoc.Object);

            var pref = new Mock<ISharedPreferences>();
            pref.Setup(a => a.GetInt("repoListCount", 10)).Returns(3);//Второй параметр =10, потому что в классе AppSettings задано дефолтное значение 10
            Ioc.RegisterSingleton(pref.Object);
        }

        [Test]
        public void TestGitRopoList()
        {
            base.Setup();
            var reader = new GitApiReader();
            var task = reader.GetRepositories();
            var list = task.Result;
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void TestGitSummary()
        {
            base.Setup();
            var reader = new GitApiReader();
            var item = reader.GitRepoSummary("mojombo/grit").Result;
            Assert.NotNull(item);
        }
    }
}
