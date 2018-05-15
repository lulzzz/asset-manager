﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManager.Core.Entities;
using AssetManager.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManager.Web.Controllers
{
    public class DepreciationController : Controller
    {
        private readonly IAsyncRepository<Depreciation> _depreciationRepository;
        public DepreciationController(IAsyncRepository<Depreciation> repository)
        {
            _depreciationRepository = repository;
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var depreciation = await _depreciationRepository.ListAllAsync();
            return View(depreciation);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var depIteam = await _depreciationRepository.GetByIdAsync(id);
            if(depIteam == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(depIteam);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert(Depreciation item)
        {
            if(item == null)
            {
                return RedirectToAction(nameof(Create));
            }
            try
            {

                await _depreciationRepository.AddAsync(item);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var dep = await _depreciationRepository.GetByIdAsync(id);
            if(dep == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(dep);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Depreciation obj)
        {
          
            try
            {
               await _depreciationRepository.UpdateAsync(obj);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}