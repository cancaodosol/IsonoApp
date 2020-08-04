using IssWebRazorApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class PlaybookService : IPlaybookService
    {
        private IPlaybookRepository _playbookRepository;
        public PlaybookService(IPlaybookRepository playbookRepository) 
        {
            _playbookRepository = playbookRepository;
        }
        public Playbook Find(int id)
        {
            var data = _playbookRepository.Find(id);
            return data.ToModel();
        }
        public List<PlaybookUnit> FindAll(PlaybookSortType type)
        {
            var playbooks = this.FindAll();
            var units = new List<PlaybookUnit>();

            switch (type) 
            {
                case PlaybookSortType.Category:
                    playbooks = playbooks.OrderBy(_ => _.Category.Code).ThenBy(__ => __.PlayName.FullName).ToList();
                    var old = "";
                    var unit = new PlaybookUnit();

                    for (int i = 0; i < playbooks.Count; i++)
                    {
                        if (i != 0 && !old.Equals(playbooks[i].Category.Code)) 
                        {
                            units.Add(unit);
                            unit = new PlaybookUnit();
                        }

                        unit.UnitTitle = playbooks[i].Category.Name;
                        unit.Playbooks.Add(playbooks[i]);
                        old = playbooks[i].Category.Code;
                    }
                    units.Add(unit);

                    break;
                case PlaybookSortType.Status:
                    playbooks = playbooks.OrderBy(_ => _.InstallStatus).ThenBy(__ => __.PlayName.FullName).ToList();
                    old = "";
                    bool isFirst = true;
                    unit = new PlaybookUnit();
                    foreach (var playbook in playbooks)
                    {
                        if (!isFirst && old != playbook.InstallStatus)
                        {
                            unit.UnitTitle = "Status : " + InstallSatusService.GetName(playbook.InstallStatus);
                            units.Add(unit);
                            old = playbook.Category.Code;
                            unit = new PlaybookUnit();
                        }
                        unit.Playbooks.Add(playbook);
                    }
                    break;
                case PlaybookSortType.FullName:
                    playbooks = playbooks.OrderBy(_ => _.PlayName.FullName).ToList();
                    unit = new PlaybookUnit();
                    unit.UnitTitle = "Sort by Play Full Name : ";
                    foreach (var playbook in playbooks)
                    {
                        unit.Playbooks.Add(playbook);
                    }
                    units.Add(unit);
                    break;
                case PlaybookSortType.ShortName:
                    playbooks = playbooks.OrderBy(_ => _.PlayName.ShortName).ToList();
                    unit = new PlaybookUnit();
                    unit.UnitTitle = "Sort by Play Short Name : ";
                    foreach (var playbook in playbooks)
                    {
                        unit.Playbooks.Add(playbook);
                    }
                    units.Add(unit);
                    break;
            }

            return units;
        }

        public List<Playbook> FindAll() 
        {
            var datas = _playbookRepository.FindAll();
            var playbooks = new List<Playbook>();

            foreach (var data in datas)
            {
                playbooks.Add(data.ToModel());
            }

            return playbooks;
        }

        /// <summary>
        /// プレイブックの新規登録
        /// </summary>
        /// <remarks>プレイデザイン画像があった場合、AWS S3に保存される。</remarks>
        /// <param name="playbook"></param>
        public async Task Add(Playbook playbook) 
        {
            var data = playbook.ToData();

            if (playbook.PlayDesign.File != null)
            {
                string uploadFilePath;
                _playbookRepository.UploadFileToS3Bucket(playbook.PlayDesign, "Offence", out uploadFilePath);
                data.PlayDesignUrl = uploadFilePath + "/" + playbook.PlayDesign.FileName;
            }

            await _playbookRepository.Add(data);
        }

        /// <summary>
        /// プレイブックの更新
        /// </summary>
        /// <remarks>プレイデザイン画像があった場合、AWS S3に保存される。</remarks>
        /// <param name="playbook"></param>
        public async Task Edit(Playbook playbook)
        {
            var data = playbook.ToData();

            if (playbook.PlayDesign.File != null)
            {
                string uploadFilePath;
                _playbookRepository.UploadFileToS3Bucket(playbook.PlayDesign, "Offence", out uploadFilePath);
                data.PlayDesignUrl = uploadFilePath + "/" + playbook.PlayDesign.FileName;
            }

            await _playbookRepository.Edit(data);
        }
        public List<Category> GetCategoryList(string session) 
        {
            var datas = _playbookRepository.GetCategoryDataList(session);
            var categories = datas.Select(_ => _.ToModel()).OrderBy(__ => __.Code).ToList<Category>();
            return categories;
        }
    }
}
