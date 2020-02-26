using System;
using System.IO;
using TextEditor.BL;

namespace TextEditor
{
    public class MainPresentor
    {
        private readonly IMainForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _messageService;

        private string _currentFilePath;

        public MainPresentor(IMainForm view, IFileManager manager, IMessageService messageService)
        {
            _view = view;
            _manager = manager;
            _messageService = messageService;

            _view.SetSymbolCount(0);
            _view.ContentChanged += _view_ContentChanged;
            _view.FileOpenClick += _view_FileOpenClick;
            _view.FileSaveClick += _view_FileSaveClick;
        }

        private void _view_ContentChanged(object sender, System.EventArgs e)
        {
            string content = _view.Content;

            int count = _manager.GetSymbolCount(content);

            _view.SetSymbolCount(count);
        }
        private void _view_FileOpenClick(object sender, System.EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool isExist = _manager.IsExist(filePath);

                if (filePath == string.Empty)
                {
                    _messageService.ShowExclamation("Please select file.");
                    return;
                }
                if (!isExist)
                {
                    _messageService.ShowExclamation("Selected file does not exist.");
                    return;
                }

                _currentFilePath = filePath;

                string content = _manager.GetContent(filePath);
                int count = _manager.GetSymbolCount(content);

                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }

        }
        private void _view_FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                string content = _view.Content;

                if (_currentFilePath == null)
                {
                    _messageService.ShowExclamation("Please open file first.");
                    return;
                }
                if (_currentFilePath != _view.FilePath)
                {
                    _messageService.ShowExclamation($"You cannot save content as a new file. File {Path.GetFileName(_currentFilePath)} is currently open.");
                    return;
                }

                _manager.SaveContent(_currentFilePath, content);

                _messageService.ShowMessage($"File {Path.GetFileName(_currentFilePath)} was saved successfully.");
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
    }
}
